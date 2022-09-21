﻿using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class OrderServiceImpl : IOrderService
{
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Order>().ToList();
    }
    
    public ActionResult<Order> GetOrderById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().Single(x => x.Id == id);
    }

    public void DeleteOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public ActionResult<Order> UpdateOrder(int id, OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        Order? oldOrder = session.Query<Order>().SingleOrDefault(x => x.Id == id);
        User? user = session.Query<User>().SingleOrDefault(x => x.Id == orderDto.UserId);
        IQueryable<Product> products = session.Query<Product>()
            .Where(x => orderDto.ProductIds.Contains(x.Id));

        if (user == null || oldOrder == null || products.IsEmpty())
        {
            throw new Exception("Not found");
        }
            
        Order order = new Order
        {
            User = user,
            Products = products.ToList(),
            CreatedAt = oldOrder.CreatedAt,
            UpdatedAt = DateTime.Now
        };
            
        session.Update(order);

        return order;
    }

    public ActionResult<Order> SaveOrder(OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        User? user = session.Query<User>().SingleOrDefault(x => x.Id == orderDto.UserId);
        IQueryable<Product> products = session.Query<Product>()
            .Where(x => orderDto.ProductIds.Contains(x.Id));

        if (user == null || products.IsEmpty())
        {
            throw new Exception("Not found");
        }
            
        Order order = new Order
        {
            User = user,
            Products = products.ToList(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        session.Save(order);
        
        return order;
    }
}