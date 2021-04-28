using System.Collections.Generic;
using System.Threading.Tasks;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.Models;

namespace SevenEleven.Services.ProductGroup
{
      public interface IProductGroup
      {
            Task<ServiceResponse<ProductGroupDto_ToReturn>> NewProductGroup(ProductGroupDto_ToCreate newProductGroup);
            Task<ServiceResponse<List<ProductGroupDto_ToReturn>>> GetAllProductGroup(bool status);
            Task<ServiceResponse<ProductGroupDto_ToReturn>> GetProductGroupById(int id);
            Task<ServiceResponse<List<ProductGroupDto_ToReturn>>> GetProductGroupByName(string name);


            Task<ServiceResponse<ProductGroupDto_ToReturn>> UpdateProductGroup(ProductGroupDto_ToUpdate productGroup, int id);
            Task<ServiceResponse<ProductGroupDto_ToReturn>> DeleteProductGroupById(int id);
            Task<ServiceResponseWithPagination<List<ProductGroupDto_ToReturn>>> SearchPagination(ProductGroupDto_Filter filter);




      }
}