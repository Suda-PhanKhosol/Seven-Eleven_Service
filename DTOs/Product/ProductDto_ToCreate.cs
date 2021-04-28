using System;
using System.ComponentModel.DataAnnotations;

namespace SevenEleven.DTOs.Product
{
      public class ProductDto_ToCreate
      {
            [Required(ErrorMessage = "Please enter Product name")]
            [MaxLength(255, ErrorMessage = "Name can not be greater than 255 charaters")]
            public string Name { get; set; }
            public float Price { get; set; }
            public int ProductGroupId { get; set; }
      }
}