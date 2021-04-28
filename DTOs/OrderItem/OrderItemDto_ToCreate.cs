using System.ComponentModel.DataAnnotations;

namespace SevenEleven.DTOs.OrderItem
{
      public class OrderItemDto_ToCreate
      {
            // [Range(1, int.MaxValue)]
            // public int OrderId { get; set; }


            [Range(1, int.MaxValue)]
            public int ProductId { get; set; }

            public float Quantity { get; set; }

            public float Total { get; set; }


      }
}