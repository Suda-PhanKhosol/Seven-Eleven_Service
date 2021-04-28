using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenEleven.DTOs.OrderItem;

namespace SevenEleven.DTOs.Order
{
      public class OrderDto_ToReturn
      {
            public float Total { get; set; }
            public float Discount { get; set; }
            public float Net { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<OrderItemDto_ToReturn> OrderItems { get; set; }
      }
}