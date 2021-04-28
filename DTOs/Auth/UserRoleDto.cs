using System;

namespace SevenEleven.DTOs
{
      public class UserRoleDto
      {
            public Guid UserId { get; set; }
            public Guid RoleId { get; set; }

            public RoleDto Role { get; set; }
      }
}