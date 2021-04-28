using System.ComponentModel.DataAnnotations;

namespace SevenEleven.DTOs.ProductGroup
{
      public class ProductGroupDto_ToCreate
      {


            [Required(ErrorMessage = "Please enter Product group name")]
            [MaxLength(255)]
            public string Name { get; set; }
      }
}