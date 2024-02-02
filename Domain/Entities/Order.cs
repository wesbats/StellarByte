using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Order
{
    public int? OrderId { get; set; }
    public int? UserId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    private int _currentItemId = 0;
    public int _nextItemId
    {
        get
        {
            _currentItemId++;
            return _currentItemId;
        }
    }
    private decimal? _price;
    public decimal? TotalPrice
    {
        get
        {
            if (OrderItems == null)
                return null;

            _price = 0;
            foreach (var item in OrderItems)
            {
                _price += item.Price;
            }
            return _price;
        }
    }
    public string? Address { get; set; }
    public int? CEP { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? FinishedDate { get; set; } = null;
}

public class OrderItem
{
    public int OrderProductId { get; set; }
    public Computer? Product { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price
    {
        get
        {
            if (Product == null || Quantity == null)
                return null;

            return Product.Price * Quantity;
        }
    }
}

public class OrderResponse
{
    public int? OrderId { get; set; }
    public int? UserId { get; set; }
    public required List<OrderItem> OrderItems { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? Address { get; set; }
    public int? CEP { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? FinishedDate { get; set; }
}