using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SevenEleven.Models
{
      [Table("ProductGroup")]

      public class ProductGroup
      {
            [Key]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }

            [Required(ErrorMessage = "Please enter Product group name")]
            [MaxLength(255)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<Product> Products { get; set; }
      }
}