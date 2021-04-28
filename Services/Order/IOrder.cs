using System.Collections.Generic;
using System.Threading.Tasks;
using SevenEleven.DTOs.Order;
using SevenEleven.DTOs.OrderItem;
using SevenEleven.Models;

namespace SevenEleven.Services.Order
{
      public interface IOrder
      {
            Task<ServiceResponse<OrderDto_ToReturn>> NewOrder(OrderDto_ToCreate newOrder);
            Task<ServiceResponse<OrderDto_ToReturn>> CancleOrder(int id);
            Task<ServiceResponse<OrderDto_ToReturn>> GetByOrderId(int orderId);
            Task<ServiceResponse<List<OrderDto_ToReturn>>> GetAllOrder(bool status);

            Task<ServiceResponse<List<OrderItemDto_ToReturn>>> GetOrderItemByOrderId(int orderId);
            Task<ServiceResponseWithPagination<List<OrderDto_ToReturn>>> SearchPagination(OrderDto_Filter filter);



      }
}