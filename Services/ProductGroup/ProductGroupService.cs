using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SevenEleven.Data;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.Models;
using mProductGroup = SevenEleven.Models.ProductGroup;

using SevenEleven.Helpers;
namespace SevenEleven.Services.ProductGroup
{
      public class ProductGroupService : ServiceBase, IProductGroup
      {

            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<ProductGroupService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public ProductGroupService(AppDBContext dbContext, IMapper mapper, ILogger<ProductGroupService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }


            public async Task<ServiceResponse<ProductGroupDto_ToReturn>> NewProductGroup(ProductGroupDto_ToCreate newProductGroup)
            {
                  var checkProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Name == newProductGroup.Name);
                  if (checkProductGroup == null)
                  {
                        var productGroup = new mProductGroup
                        {
                              Name = newProductGroup.Name,
                              IsActive = true,
                              CreatedDate = Now(),
                        };
                        _dbContext.ProductGroups.Add(productGroup);
                        await _dbContext.SaveChangesAsync();
                        return ResponseResult.Success(_mapper.Map<ProductGroupDto_ToReturn>(productGroup));
                  }
                  else
                  {
                        return ResponseResult.Failure<ProductGroupDto_ToReturn>("There is already a product group with the same name.");

                  }
            }
            public async Task<ServiceResponse<List<ProductGroupDto_ToReturn>>> GetAllProductGroup(bool status)
            {

                  if (status)
                  {
                        return ResponseResult.Success(
                                 _mapper.Map<List<ProductGroupDto_ToReturn>>
                                 (await _dbContext.ProductGroups
                                 .AsNoTracking().Where(x => x.IsActive == true)
                                 .ToListAsync())
                        );
                  }
                  else
                  {
                        return ResponseResult.Success(
                                _mapper.Map<List<ProductGroupDto_ToReturn>>
                                (await _dbContext.ProductGroups
                                .AsNoTracking().Where(x => x.IsActive == false)
                                .ToListAsync())
                       );
                  }
            }

            public async Task<ServiceResponse<ProductGroupDto_ToReturn>> GetProductGroupById(int id)
            {
                  var productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == id);
                  if (productGroup != null)
                  {
                        if (productGroup.IsActive != false)
                        {
                              return ResponseResult.Success(_mapper.Map<ProductGroupDto_ToReturn>(productGroup));
                        }
                        else
                        {
                              return ResponseResult.Failure<ProductGroupDto_ToReturn>("Product group has been deleted");
                        }

                  }
                  else
                  {
                        return ResponseResult.Failure<ProductGroupDto_ToReturn>("Not found product group id");
                  }
            }

            public async Task<ServiceResponse<List<ProductGroupDto_ToReturn>>> GetProductGroupByName(string name)
            {
                  var productGroup = await _dbContext.ProductGroups.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<ProductGroupDto_ToReturn>>(productGroup));

            }
            public async Task<ServiceResponse<ProductGroupDto_ToReturn>> UpdateProductGroup(ProductGroupDto_ToUpdate productGroup, int id)
            {
                  var oldProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == id);
                  if (productGroup != null)
                  {
                        oldProductGroup.IsActive = productGroup.IsActive;
                        oldProductGroup.Name = productGroup.Name;
                        await _dbContext.SaveChangesAsync();
                        return ResponseResult.Success(_mapper.Map<ProductGroupDto_ToReturn>(await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == id)));
                  }
                  else
                  {
                        return ResponseResult.Failure<ProductGroupDto_ToReturn>("Not found product group id");
                  }
            }

            public async Task<ServiceResponse<ProductGroupDto_ToReturn>> DeleteProductGroupById(int id)
            {
                  var productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == id);
                  if (productGroup != null)
                  {
                        if (productGroup.IsActive != false)
                        {
                              productGroup.IsActive = false;
                              await _dbContext.SaveChangesAsync();
                              return ResponseResult.Success(_mapper.Map<ProductGroupDto_ToReturn>(productGroup));
                        }
                        else
                        {
                              return ResponseResult.Failure<ProductGroupDto_ToReturn>("Product group has been deleted");
                        }

                  }
                  else
                  {
                        return ResponseResult.Failure<ProductGroupDto_ToReturn>("Not found product group id");
                  }
            }

            public async Task<ServiceResponseWithPagination<List<ProductGroupDto_ToReturn>>> SearchPagination(ProductGroupDto_Filter filter)
            {
                  var productGroup = _dbContext.ProductGroups.AsQueryable();

                  if (!string.IsNullOrWhiteSpace(filter.Name))
                  {
                        productGroup = productGroup.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
                  }
                  productGroup = productGroup.Where(x => x.IsActive == filter.IsActive);


                  if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                  {
                        try
                        {
                              productGroup = productGroup.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                        }
                        catch
                        {
                              return ResponseResultWithPagination.Failure<List<ProductGroupDto_ToReturn>>($"Could not order by field: {filter.OrderingField}");
                        }
                  }

                  var paginationResult = await _httpContext.HttpContext.InsertPaginationParametersInResponse(productGroup, filter.RecordsPerPage, filter.Page);



                  var result = _mapper.Map<List<ProductGroupDto_ToReturn>>(await productGroup.Paginate(filter).ToListAsync());
                  return ResponseResultWithPagination.Success(result, paginationResult);
            }
      }
}