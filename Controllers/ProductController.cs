using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenEleven.DTOs.Product;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.Services.Product;

namespace SevenEleven.Controllers
{
      [ApiController]
      [Route("api/[controller]")]


      public class ProductController : ControllerBase
      {
            private readonly IProduct _productService;

            public ProductController(IProduct productService)
            {
                  _productService = productService;
            }
            [HttpPost("NewProduct")]
            [Authorize(Roles = "Admin,Manager,Developer")]
            public async Task<IActionResult> NewProduct(ProductDto_ToCreate newProduct)
            {
                  return Ok(await _productService.NewProduct(newProduct));
            }


            [HttpGet("GetAllProduct/status")]
            public async Task<IActionResult> GetAllProduct(bool status)
            {
                  return Ok(await _productService.GetAllProduct(status));
            }


            [HttpGet("GetProductById/id")]
            [Authorize(Roles = "Admin,Manager,Developer")]
            public async Task<IActionResult> GetProductById(int id)
            {
                  return Ok(await _productService.GetProductById(id));
            }


            [HttpGet("GetProductByName/name")]
            public async Task<IActionResult> GetProductByName(string name)
            {
                  return Ok(await _productService.GetProductByName(name));
            }



            [HttpPut("UpdateProduct/id")]
            [Authorize]
            public async Task<IActionResult> UpdateProduct(ProductDto_ToUpdate product, int id)
            {
                  return Ok(await _productService.UpdateProduct(product, id));
            }


            [HttpPut("DeleteProductById/id")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> DeleteProductById(int id)
            {
                  return Ok(await _productService.DeleteProductById(id));
            }



            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] ProductDto_Filter filter)
            {
                  return Ok(await _productService.SearchPagination(filter));
            }
      }
}