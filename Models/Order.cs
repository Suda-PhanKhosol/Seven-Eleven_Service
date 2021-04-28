using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SevenEleven.Models
{
      [Table("Order")]
      public class Order
      {

            [Key]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }
            public float Total { get; set; }
            public float Discount { get; set; }
            public float Net { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<OrderItem> OrderItems { get; set; }

      }
}