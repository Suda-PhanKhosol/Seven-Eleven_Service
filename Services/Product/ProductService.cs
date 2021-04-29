using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SevenEleven.Data;
using SevenEleven.Models;
using SevenEleven.Services;
using SevenEleven.DTOs.Product;
using mProduct = SevenEleven.Models.Product;
using System.Linq;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.Helpers;
using System.Linq.Dynamic.Core;

namespace SevenEleven.Services.Product
{
      public class ProductService : ServiceBase, IProduct
      {

            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<ProductService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public ProductService(AppDBContext dbContext, IMapper mapper, ILogger<ProductService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }


            public async Task<ServiceResponse<ProductDto_ToReturn>> NewProduct(ProductDto_ToCreate newProduct)
            {
                  string errorMessage = "";
                  if (newProduct.Price == 0)
                  {
                        errorMessage += "Please enter price field";
                        return ResponseResult.Failure<ProductDto_ToReturn>(errorMessage);
                  }
                  if (newProduct.ProductGroupId == 0)
                  {
                        errorMessage += "Please enter product group id field";
                        return ResponseResult.Failure<ProductDto_ToReturn>(errorMessage);
                  }

                  var checkProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == newProduct.Name);
                  if (checkProduct == null)
                  {
                        var product = new mProduct
                        {
                              Name = newProduct.Name,
                              Price = newProduct.Price,
                              CreatedDate = Now(),
                              IsActive = true,
                              ProductGroupId = newProduct.ProductGroupId,
                        };
                        _dbContext.Products.Add(product);
                        await _dbContext.SaveChangesAsync();

                        return ResponseResult.Success(_mapper.Map<ProductDto_ToReturn>(product));
                  }
                  else
                  {
                        return ResponseResult.Failure<ProductDto_ToReturn>("There is already a product group with the same name.");
                  }
            }
            public async Task<ServiceResponse<List<ProductDto_ToReturn>>> GetAllProduct(bool status)
            {
                  if (status)
                  {
                        return ResponseResult.Success(
                                 _mapper.Map<List<ProductDto_ToReturn>>
                                 (await _dbContext.Products.Include(x => x.ProductGroups)
                                 .AsNoTracking().Where(x => x.IsActive == true)
                                 .ToListAsync())
                        );
                  }
                  else
                  {
                        return ResponseResult.Success(
                                _mapper.Map<List<ProductDto_ToReturn>>
                                (await _dbContext.Products.Include(x => x.ProductGroups)
                                .AsNoTracking().Where(x => x.IsActive == false)
                                .ToListAsync())
                       );
                  }
            }
            public async Task<ServiceResponse<ProductDto_ToReturn>> GetProductById(int id)
            {
                  var product = await _dbContext.Products.Include(x => x.ProductGroups).FirstOrDefaultAsync(x => x.Id == id);
                  if (product != null)
                  {
                        if (product.IsActive != false)
                        {
                              return ResponseResult.Success(_mapper.Map<ProductDto_ToReturn>(product));
                        }
                        else
                        {
                              return ResponseResult.Failure<ProductDto_ToReturn>("Product has been deleted");
                        }

                  }
                  else
                  {
                        return ResponseResult.Failure<ProductDto_ToReturn>("Not found product id");
                  }
            }

            public async Task<ServiceResponse<List<ProductDto_ToReturn>>> GetProductByName(string name)
            {

                  var product = await _dbContext.Products.Include(x => x.ProductGroups).Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<ProductDto_ToReturn>>(product));
            }

            public async Task<ServiceResponse<ProductDto_ToReturn>> UpdateProduct(ProductDto_ToUpdate product, int id)
            {
                  var oldProduct = await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                  if (oldProduct != null)
                  {
                        oldProduct.Name = product.Name;
                        oldProduct.Price = product.Price;
                        oldProduct.IsActive = product.IsActive;
                        oldProduct.ProductGroupId = product.ProductGroupId;
                        await _dbContext.SaveChangesAsync();
                        return ResponseResult.Success(_mapper.Map<ProductDto_ToReturn>
                        (
                                  await _dbContext.Products.Include(x => x.ProductGroups).Where(x => x.Id == id).FirstOrDefaultAsync()
                        ));
                  }
                  else
                  {
                        return ResponseResult.Failure<ProductDto_ToReturn>("Not found product id");
                  }
            }

            public async Task<ServiceResponse<ProductDto_ToReturn>> DeleteProductById(int id)
            {
                  //ทำแบบนี้แล้วไม่ได้ AsNoTracking() ใช้ได้แค่กับ List ===> var product = await _dbContext.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
                  var product = await _dbContext.Products.Include(x => x.ProductGroups).FirstOrDefaultAsync(x => x.Id == id);

                  if (product != null)
                  {
                        if (product.IsActive != false)
                        {
                              product.IsActive = false;
                              await _dbContext.SaveChangesAsync();
                              return ResponseResult.Success(_mapper.Map<ProductDto_ToReturn>(product));
                        }
                        else
                        {
                              return ResponseResult.Failure<ProductDto_ToReturn>("Product has been deleted");
                        }
                  }
                  else
                  {
                        return ResponseResult.Failure<ProductDto_ToReturn>("Not found product id");
                  }
            }

            public async Task<ServiceResponseWithPagination<List<ProductDto_ToReturn>>> SearchPagination(ProductDto_Filter filter)
            {
                  var product = _dbContext.Products.Include(x => x.ProductGroups).AsQueryable();


                  if (!string.IsNullOrWhiteSpace(filter.Name))
                  {
                        product = product.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
                  }
                  product = product.Where(x => x.Price <= filter.MaxPrice);
                  product = product.Where(x => x.IsActive == filter.IsActive);


                  if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                  {
                        try
                        {
                              product = product.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                        }
                        catch
                        {
                              return ResponseResultWithPagination.Failure<List<ProductDto_ToReturn>>($"Could not order by field: {filter.OrderingField}");
                        }
                  }


                  var paginationResult = await _httpContext.HttpContext.InsertPaginationParametersInResponse(product, filter.RecordsPerPage, filter.Page);


                  var result = _mapper.Map<List<ProductDto_ToReturn>>(await product.Paginate(filter).ToListAsync());
                  return ResponseResultWithPagination.Success(result, paginationResult);

            }
      }
}
