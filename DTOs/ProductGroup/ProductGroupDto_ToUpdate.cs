using System;
using System.ComponentModel.DataAnnotations;

namespace SevenEleven.DTOs.ProductGroup
{
      public class ProductGroupDto_ToUpdate
      {
            [Required(ErrorMessage = "Please enter Product group name")]
            [MaxLength(255)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }
      }
}