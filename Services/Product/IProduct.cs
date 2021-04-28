using System.Collections.Generic;
using System.Threading.Tasks;
using SevenEleven.Models;
using SevenEleven.DTOs.Product;
using SevenEleven.DTOs.ProductGroup;

namespace SevenEleven.Services.Product
{
      public interface IProduct
      {
            Task<ServiceResponse<ProductDto_ToReturn>> NewProduct(ProductDto_ToCreate newProduct);
            Task<ServiceResponse<List<ProductDto_ToReturn>>> GetAllProduct(bool status);
            Task<ServiceResponse<ProductDto_ToReturn>> GetProductById(int id);
            Task<ServiceResponse<List<ProductDto_ToReturn>>> GetProductByName(string name);
            Task<ServiceResponse<ProductDto_ToReturn>> UpdateProduct(ProductDto_ToUpdate product, int id);
            Task<ServiceResponse<ProductDto_ToReturn>> DeleteProductById(int id);
            Task<ServiceResponseWithPagination<List<ProductDto_ToReturn>>> SearchPagination(ProductDto_Filter filter);


      }
}