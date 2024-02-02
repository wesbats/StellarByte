using Domain.Entities;
using Domain.Request;

namespace Domain.Mappers;

public class OrderMapper
{
    public static Order ToEntity(BaseOrderRequest order) => new Order
    {
        UserId = order.UserId,
        Address = order.Addres,
        CEP = order.CEP
    };
    public static OrderItem ToEntity<T>(BaseOrderItem order, T item) where T : Computer => new OrderItem
    {
        OrderProductId = (int)order.ProductId!,
        Quantity = order.Quantity,
        Product = item
    };

    public static OrderResponse ToResponse(Order order) => new OrderResponse
    {
        OrderId = order.OrderId,
        UserId = order.OrderId,
        OrderItems = order.OrderItems,
        TotalPrice = order.TotalPrice,
        Address = order.Address,
        CEP = order.CEP,
        OrderDate = order.OrderDate,
        FinishedDate = order.FinishedDate
    };
}
