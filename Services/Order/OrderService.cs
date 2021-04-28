using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SevenEleven.Data;
using SevenEleven.DTOs.Order;
using SevenEleven.Models;
using mOrder = SevenEleven.Models.Order;
using mOrderItem = SevenEleven.Models.OrderItem;
using SevenEleven.Helpers;
using System.Linq.Dynamic.Core;


namespace SevenEleven.Services.Order
{
      public class OrderService : ServiceBase, IOrder
      {

            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<OrderService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public OrderService(AppDBContext dbContext, IMapper mapper, ILogger<OrderService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }


            public async Task<ServiceResponse<OrderDto_ToReturn>> NewOrder(OrderDto_ToCreate newOrder)
            {
                  //Validate OrderItem
                  var errorMessage = "";
                  bool checkInput = false;
                  float totalCal = 0;
                  foreach (var item in newOrder.OrderItems)
                  {
                        var product = await _dbContext.Products
                        .FirstOrDefaultAsync(x => x.Id == item.ProductId && x.IsActive == true);
                        if (product != null)
                        {
                              if (item.Total != product.Price * item.Quantity)
                              {
                                    errorMessage += "Incorrct calculate total order item.";
                                    checkInput = true;
                              }
                              if (checkInput)
                              {
                                    errorMessage += "Product Id = " + item.ProductId.ToString();
                                    return ResponseResult.Failure<OrderDto_ToReturn>(errorMessage);
                              }
                              else
                              {
                                    totalCal += product.Price * item.Quantity;
                              }
                        }
                        else
                        {
                              errorMessage += "Can not find product Id = " + item.ProductId.ToString();
                              return ResponseResult.Failure<OrderDto_ToReturn>(errorMessage);
                        }
                  }

                  //Validate OrderHeader
                  if (totalCal != newOrder.Total)
                  {
                        errorMessage += "Incorrct calculate total order.";
                        return ResponseResult.Failure<OrderDto_ToReturn>(errorMessage);
                  }
                  else
                  {
                        if (newOrder.Net != totalCal - newOrder.Discount)
                        {
                              errorMessage += "Incorrct calculate net.";
                              return ResponseResult.Failure<OrderDto_ToReturn>(errorMessage);
                        }
                        else
                        {
                              //InsertOrder
                              var order = new mOrder
                              {
                                    Total = totalCal,
                                    Discount = newOrder.Discount,
                                    Net = totalCal - newOrder.Discount,
                                    CreatedDate = Now(),
                                    IsActive = true,
                              };
                              _dbContext.Orders.Add(order);
                              await _dbContext.SaveChangesAsync();

                              //InsertOrderItem
                              List<mOrderItem> ordetItem = new List<mOrderItem>();
                              foreach (var item in newOrder.OrderItems)
                              {
                                    var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                                    ordetItem.Add(new mOrderItem()
                                    {
                                          OrderId = order.Id,
                                          ProductId = item.ProductId,
                                          Price = product.Price,
                                          Quantity = item.Quantity,
                                          Total = item.Total
                                    });
                              }
                              _dbContext.OrderItems.AddRange(ordetItem);
                              await _dbContext.SaveChangesAsync();


                              //Include List
                              int maxId = await _dbContext.Orders.MaxAsync(x => x.Id);
                              var getLastOrder = await _dbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == maxId);


                              return ResponseResult.Success(_mapper.Map<OrderDto_ToReturn>(order));
                        }
                  }

            }
            public async Task<ServiceResponse<List<OrderDto_ToReturn>>> GetAllOrder(bool status)
            {
                  if (status)
                  {
                        var order = await _dbContext.Orders
                        .Include(x => x.OrderItems).ThenInclude(x => x.Products)
                        .Where(X => X.IsActive == true)
                        .ToListAsync();
                        return ResponseResult.Success(_mapper.Map<List<OrderDto_ToReturn>>(order));
                  }
                  else
                  {
                        var order = await _dbContext.Orders
                       .Include(x => x.OrderItems).ThenInclude(x => x.Products)
                       .Where(X => X.IsActive == false)
                       .ToListAsync();
                        return ResponseResult.Success(_mapper.Map<List<OrderDto_ToReturn>>(order));
                  }
            }
            public async Task<ServiceResponse<OrderDto_ToReturn>> GetByOrderId(int orderId)
            {
                  var order = await _dbContext.Orders
                  .Include(x => x.OrderItems).ThenInclude(x => x.Products)
                  .Where(x => x.Id == orderId).FirstOrDefaultAsync();
                  if (order.IsActive != false)
                  {
                        return ResponseResult.Success(_mapper.Map<OrderDto_ToReturn>(order));
                  }
                  else
                  {
                        return ResponseResult.Failure<OrderDto_ToReturn>("Order has been cancled");

                  }
            }
            public async Task<ServiceResponse<List<DTOs.OrderItem.OrderItemDto_ToReturn>>> GetOrderItemByOrderId(int orderId)
            {
                  var orderItem = await _dbContext.OrderItems
                  .Include(x => x.Products)
                  .Where(x => x.OrderId == orderId).ToListAsync();

                  var orderHeader = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderItem[0].OrderId);
                  if (orderHeader.IsActive != false)
                  {
                        return ResponseResult.Success(_mapper.Map<List<DTOs.OrderItem.OrderItemDto_ToReturn>>(orderItem));
                  }
                  else
                  {
                        return ResponseResult.Failure<List<DTOs.OrderItem.OrderItemDto_ToReturn>>("Order has been cancled");
                  }
            }

            public async Task<ServiceResponse<OrderDto_ToReturn>> CancleOrder(int id)
            {
                  var order = await _dbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
                  if (order != null)
                  {
                        if (order.IsActive != false)
                        {
                              order.IsActive = false;
                              await _dbContext.SaveChangesAsync();

                              return ResponseResult.Success(_mapper.Map<OrderDto_ToReturn>(order));
                        }
                        else
                        {
                              return ResponseResult.Failure<OrderDto_ToReturn>("Product has been deleted");

                        }
                  }
                  else
                  {
                        return ResponseResult.Failure<OrderDto_ToReturn>("Not found product id");

                  }
            }

            public async Task<ServiceResponseWithPagination<List<OrderDto_ToReturn>>> SearchPagination(OrderDto_Filter filter)
            {
                  var order = _dbContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Products).AsQueryable();
                  order = order.Where(x => x.IsActive == filter.IsActive);

                  // 2. Order => Order by
                  if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                  {
                        try
                        {
                              order = order.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                        }
                        catch
                        {
                              return ResponseResultWithPagination.Failure<List<OrderDto_ToReturn>>($"Could not order by field: {filter.OrderingField}");
                        }
                  }

                  var paginationResult = await _httpContext.HttpContext.InsertPaginationParametersInResponse(order, filter.RecordsPerPage, filter.Page);
                  // var custom = await cus.Paginate(filter).ToListAsync();
                  var result = _mapper.Map<List<OrderDto_ToReturn>>(await order.Paginate(filter).ToListAsync());
                  return ResponseResultWithPagination.Success(result, paginationResult);
            }
      }
}