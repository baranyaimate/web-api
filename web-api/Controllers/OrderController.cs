using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: api/order
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        return _orderService.GetAll();
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        return _orderService.GetOrderById(id);
    }

    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public ActionResult<Order> UpdateOrder(int id, OrderDto orderDto)
    {
        try
        {
            return _orderService.UpdateOrder(id, orderDto);
        }
        catch
        {
            return NotFound();
        }
    }

    // POST: api/order
    [HttpPost]
    public ActionResult<Order> SaveOrder(OrderDto orderDto)
    {
        return _orderService.SaveOrder(orderDto);
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public void DeleteOrder(int id)
    {
        _orderService.DeleteOrder(id);
    }
}