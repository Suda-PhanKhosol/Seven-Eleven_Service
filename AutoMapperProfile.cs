using AutoMapper;
using SevenEleven.DTOs;
using SevenEleven.Models;
using SevenEleven.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenEleven.DTOs.ProductGroup;
using SevenEleven.DTOs.Order;
using SevenEleven.DTOs.OrderItem;

namespace SevenEleven
{
      public class AutoMapperProfile : Profile
      {
            public AutoMapperProfile()
            {
                  CreateMap<User, UserDto>();
                  CreateMap<Role, RoleDto>()
                      .ForMember(x => x.RoleName, x => x.MapFrom(x => x.Name));
                  CreateMap<RoleDtoAdd, Role>()
                      .ForMember(x => x.Name, x => x.MapFrom(x => x.RoleName)); ;
                  CreateMap<UserRole, UserRoleDto>();


                  CreateMap<Product, ProductDto_ToReturn>().ReverseMap();
                  CreateMap<OrderItem, OrderItemDto_ToReturn>().ReverseMap();
                  CreateMap<OrderItem, OrderItemDto_ToReturnByOrderId>().ReverseMap();


                  CreateMap<ProductGroup, ProductGroupDto_ToReturn>().ReverseMap();
                  CreateMap<Order, OrderDto_ToReturn>().ReverseMap();
                  CreateMap<Order, OrderItemDto_ToReturnByOrderId>().ReverseMap();






            }
      }
}