using Domain.Entities;
using Domain.Exceptions;
using Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories;

public interface IOrderRepository
{
    List<Order> ListOrders();
    Order? GetOrder(int id);
    List<OrderItem> GetOrderItems(int idOrder);
    OrderItem? GetOrderItem(int idOrder, int idItem);
    Order Create(Order newOrder);
    Order UpdateAddress(UpdatedOrderAddressRequest updatedOrder);
    Order AddItem (int orderId, OrderItem Item);
    Order RemoveItem(int orderId, int item);
    Order FinalizePayment(Order order);
    void Delete(int Id);
}

public class OrderRepository : IOrderRepository
{
    private List<Order> _orders = new List<Order>();
    private int _currentId = 0;
    private int _nextId
    {
        get
        {
            _currentId++;
            return _currentId;
        }
    }


    public List<Order> ListOrders()
    {
        return _orders;
    }

    public Order? GetOrder(int id)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == id);
        return order;
    }

    public List<OrderItem> GetOrderItems(int orderId)
    {
        var order = GetOrder(orderId);
        if (order is null)
            throw new NotFoundException("Order is invalid");

        var orderItems = order.OrderItems;
        return orderItems;
    }

    public OrderItem? GetOrderItem(int orderId, int itemId)
    {
        var order = GetOrder(orderId);
        if (order is null)
            throw new NotFoundException("Order is invalid!");

        var orderItem = order.OrderItems.FirstOrDefault(i => i.OrderProductId == itemId);
        return orderItem ?? null;
    }

    public Order Create(Order newOrder)
    {
        newOrder.OrderId = _nextId;
        _orders.Add(newOrder);
        return newOrder;
    }

    public Order UpdateAddress(UpdatedOrderAddressRequest updatedOrderAddress)
    {
        var order = GetOrder((int)updatedOrderAddress.OrderId!);
        if (order is null)
            throw new NotFoundException("OrderId is invalid!");

        order.Address = updatedOrderAddress.Addres;
        order.CEP = updatedOrderAddress.CEP;

        return order;
    }

    public Order AddItem(int orderId, OrderItem Item)
    {
        var order = GetOrder(orderId);
        if (order is null)
            throw new NotFoundException("Order is invalid!");

        Item.OrderProductId = order._nextItemId;
        order.OrderItems.Add(Item);
        return order;
    }

    public Order RemoveItem(int orderId, int itemId)
    {
        var order = GetOrder(orderId);
        if (order is null)
            throw new NotFoundException("Order is invalid!");

        var item = order.OrderItems.FirstOrDefault(i => i.OrderProductId == itemId);
        if (item is null)
            throw new BadRequestException("productId", "productId is invalid!");

        order.OrderItems.Remove(item!);
        return order;
    }

    public Order FinalizePayment(Order order)
    {
        order.FinishedDate = DateTime.Now;
        return order;
    }

    public void Delete(int idOrder)
    {
        var order = GetOrder(idOrder);
        if (order is null)
            throw new NotFoundException("Order is invalid!");

        _orders.Remove(order);
    }
}
