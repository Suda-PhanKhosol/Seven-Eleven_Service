using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SevenEleven.Models
{
      [Table("Product")]

      public class Product
      {
            [Key]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }

            [Required(ErrorMessage = "Please enter Product name")]
            [MaxLength(255, ErrorMessage = "Name can not be greater than 255 charaters")]
            public string Name { get; set; }
            public float Price { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }

            public DateTime CreatedDate { get; set; }

            [Range(1, int.MaxValue)]
            public int ProductGroupId { get; set; }
            public ProductGroup ProductGroups { get; set; }

      }
}