using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenEleven.DTOs.Product;

namespace SevenEleven.DTOs.ProductGroup
{
      public class ProductGroupDto_ToReturn
      {

            [Required(ErrorMessage = "Please enter Product group name")]
            [MaxLength(255)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please enter Active status")]
            public Boolean IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            // public List<ProductDto_ToReturn> Products { get; set; }
      }
}