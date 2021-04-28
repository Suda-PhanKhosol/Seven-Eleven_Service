using System.Collections.Generic;
using SevenEleven.DTOs.OrderItem;

namespace SevenEleven.DTOs.Order
{
      public class OrderDto_ToCreate
      {
            public List<OrderItemDto_ToCreate> OrderItems { get; set; }

            public float Total { get; set; }
            public float Discount { get; set; }
            public float Net { get; set; }
      }
}