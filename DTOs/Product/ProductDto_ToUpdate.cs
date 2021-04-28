using System;
using System.ComponentModel.DataAnnotations;

namespace SevenEleven.DTOs.Product
{
      public class ProductDto_ToUpdate
      {
            [Required(ErrorMessage = "Please enter Product name")]
            [MaxLength(255, ErrorMessage = "Name can not be greater than 255 charaters")]
            public string Name { get; set; }
            public float Price { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }

            [Range(1, int.MaxValue)]
            public int ProductGroupId { get; set; }
      }
}