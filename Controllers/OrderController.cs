using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenEleven.DTOs.Order;
using SevenEleven.Services.Order;

namespace SevenEleven.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      [Authorize]

      public class OrderController : ControllerBase
      {

            private readonly IOrder _orderService;

            public OrderController(IOrder orderService)
            {
                  this._orderService = orderService;
            }
            [HttpPost("NewOrder")]
            public async Task<IActionResult> NewOrder(OrderDto_ToCreate newProduct)
            {
                  return Ok(await _orderService.NewOrder(newProduct));
            }
            [HttpGet("GetAllOrder/status")]
            public async Task<IActionResult> GetAllOrder(bool status)
            {
                  return Ok(await _orderService.GetAllOrder(status));
            }
            [HttpGet("GetOrderItemByOrderId/id")]
            public async Task<IActionResult> GetOrderItemByOrderId(int id)
            {
                  return Ok(await _orderService.GetOrderItemByOrderId(id));
            }
            [HttpGet("GetByOrderId/id")]
            public async Task<IActionResult> GetByOrderId(int id)
            {
                  return Ok(await _orderService.GetByOrderId(id));
            }
            [HttpPut("CancleOrder/id")]
            public async Task<IActionResult> CancleOrder(int id)
            {
                  return Ok(await _orderService.CancleOrder(id));
            }
            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] OrderDto_Filter filter)
            {
                  return Ok(await _orderService.SearchPagination(filter));
            }

      }
}