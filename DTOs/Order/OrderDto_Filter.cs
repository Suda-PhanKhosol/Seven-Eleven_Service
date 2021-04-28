using System;

namespace SevenEleven.DTOs.Order
{
      public class OrderDto_Filter : PaginationDto
      {
            public Boolean IsActive { get; set; }
      }
}