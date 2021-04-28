using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.Services.ProductGroup;

namespace SevenEleven.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class ProductGroupController : ControllerBase
      {
            private readonly IProductGroup _productGroupService;

            public ProductGroupController(IProductGroup productGroupService)
            {
                  _productGroupService = productGroupService;
            }
            [HttpPost("NewProductGroup")]
            [Authorize(Roles = "Admin,Manager,Developer")]
            public async Task<IActionResult> NewProductGroup(ProductGroupDto_ToCreate newProductGroup)
            {
                  return Ok(await _productGroupService.NewProductGroup(newProductGroup));
            }
            [HttpGet("GetAllProductGroup/status")]
            public async Task<IActionResult> GetAllProductGroup(bool status)
            {
                  return Ok(await _productGroupService.GetAllProductGroup(status));
            }
            [HttpGet("GetProductGroupById/id")]
            [Authorize(Roles = "Admin,Manager,Developer")]
            public async Task<IActionResult> GetProductGroupById(int id)
            {
                  return Ok(await _productGroupService.GetProductGroupById(id));
            }
            [HttpGet("GetProductGroupByName/name")]
            public async Task<IActionResult> GetProductGroupByName(string name)
            {
                  return Ok(await _productGroupService.GetProductGroupByName(name));
            }
            [HttpPut("UpdateProductGroup/id")]
            [Authorize(Roles = "Admin,Manager,Developer")]
            public async Task<IActionResult> UpdateProductGroup(ProductGroupDto_ToUpdate productGroup, int id)
            {
                  return Ok(await _productGroupService.UpdateProductGroup(productGroup, id));
            }


            [HttpPut("DeleteProductGroupById/id")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> DeleteProductGroupById(int id)
            {
                  return Ok(await _productGroupService.DeleteProductGroupById(id));
            }
            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] ProductGroupDto_Filter filter)
            {
                  return Ok(await _productGroupService.SearchPagination(filter));
            }

      }
}