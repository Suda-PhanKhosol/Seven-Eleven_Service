using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenEleven.DTOs.Order;
using SevenEleven.DTOs.Product;

namespace SevenEleven.DTOs.OrderItem
{
      public class OrderItemDto_ToReturn
      {

            public float Price { get; set; }
            public float Quantity { get; set; }

            public float Total { get; set; }

            [Range(1, int.MaxValue)]
            public int OrderId { get; set; }

            // public OrderDto_ToReturn OrderDto_ToReturns { get; set; }

            [Range(1, int.MaxValue)]
            public int ProductId { get; set; }
            public ProductDto_ToReturn Products { get; set; }
      }
}