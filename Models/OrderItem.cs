using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SevenEleven.Models
{
      [Table("OrderItem")]

      public class OrderItem
      {
            [Key]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }
            public float Price { get; set; }
            public float Quantity { get; set; }

            public float Total { get; set; }

            [Range(1, int.MaxValue)]
            public int OrderId { get; set; }

            public Order Orders { get; set; }

            [Range(1, int.MaxValue)]
            public int ProductId { get; set; }
            public Product Products { get; set; }


      }
}