using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IOrderService
{
    ActionResult<IEnumerable<Order>> GetAll();
    
    ActionResult<Order> GetOrderById(int id);

    ActionResult<Order> UpdateOrder(int id, OrderDto orderDto);
    
    ActionResult<Order> SaveOrder(OrderDto orderDto);

    void DeleteOrder(int id);
}