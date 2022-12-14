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
    public IEnumerable<Order> GetAll()
    {
        return _orderService.GetAll();
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        try
        {
            return _orderService.GetOrderById(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public ActionResult<Order> UpdateOrder(int id, OrderDto orderDto)
    {
        try
        {
            return _orderService.UpdateOrder(id, orderDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/order
    [HttpPost]
    public ActionResult<Order> SaveOrder(OrderDto orderDto)
    {
        try
        {
            return _orderService.SaveOrder(orderDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id)
    {
        try
        {
            _orderService.DeleteOrder(id);
            return Ok("The order was deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}