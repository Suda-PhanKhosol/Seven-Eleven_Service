using System.Collections.Generic;
using SevenEleven.DTOs.OrderItem;

namespace SevenEleven.DTOs.Order
{
      public class OrderItemDto_ToReturnByOrderId
      {
            public List<OrderItemDto_ToReturn> OrderItems { get; set; }

      }
}