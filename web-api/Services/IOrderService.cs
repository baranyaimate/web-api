using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IOrderService
{
    IEnumerable<Order> GetAll();

    Order GetOrderById(int id);

    Order UpdateOrder(int id, OrderDto orderDto);

    Order SaveOrder(OrderDto orderDto);

    void DeleteOrder(int id);
}