using Domain.Entities;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services;

public interface IOrderService
{
    List<OrderResponse> ListOrders();
    OrderResponse? GetOrder(int id);
    List<OrderItem> GetOrderItems(int orderId);
    OrderItem GetOrderItem(int orderId, int itemId);
    OrderResponse Create(BaseOrderRequest newOrder);
    OrderResponse AddItem(int orderId, BaseOrderItem item);
    void RemoveItem(int orderId, int itemId);
    OrderResponse UpdateAddress(UpdatedOrderAddressEntity updatedOrder);
    OrderResponse FinalizePayment(int orderID);
    void Delete(int orderID);
}

public class OrderService : IOrderService
{
    private readonly IValidator<BaseOrderRequest> _orderValidator;
    private readonly IValidator<BaseOrderItem> _itemValidator;
    private readonly IValidator<UpdatedOrderAddressEntity> _addressValidator;
    private readonly IOrderRepository _repoOrder;
    private readonly IComputerRepository _repoComputer;
    private readonly IUserService _userService;

    public OrderService(IValidator<BaseOrderRequest> orderValidator, IValidator<BaseOrderItem> itemValidator,
                        IValidator<UpdatedOrderAddressEntity> addressValidator,
                        IOrderRepository repository, IComputerRepository repoComputer, IUserService userService)
    {
        _orderValidator = orderValidator;
        _itemValidator = itemValidator;
        _addressValidator = addressValidator;
        _repoOrder = repository;
        _repoComputer = repoComputer;
        _userService = userService;
    }

    public List<OrderResponse> ListOrders()
    {
        var orders = _repoOrder.ListOrders();
        List<OrderResponse> response = new();
        orders.ForEach(o => response.Add(OrderMapper.ToResponse(o)));
        return response;
    }

    public OrderResponse GetOrder(int id)
    {
        var order = _repoOrder.GetOrder(id);
        if (order is null)
            throw new BadRequestException("Order", "Order is invalid!");
        var response = OrderMapper.ToResponse(order);
        return response;
    }

    public OrderResponse Create(BaseOrderRequest request)
    {
        var Errors = _orderValidator.Validate(request);
        if (Errors.Any())
            throw new BadRequestException(Errors);

        var user = _userService.GetById((int)request.UserId!);
        if (user is null)
            throw new BadRequestException("userId", "UserId is invalid!");

        var newOrder = OrderMapper.ToEntity(request);
        _repoOrder.Create(newOrder);
        var response = OrderMapper.ToResponse(newOrder);
        return response;
    }

    public List<OrderItem> GetOrderItems(int orderID)
    {
        var orderItems = _repoOrder.GetOrderItems(orderID);
        return orderItems;
    }

    public OrderItem GetOrderItem(int orderID, int idItem)
    {
        var orderItem = _repoOrder.GetOrderItem(orderID, idItem);
        if (orderItem is null)
            throw new BadRequestException("ItemId", "ItemId is invalid!");

        return orderItem!;
    }

    public OrderResponse AddItem(int orderId, BaseOrderItem baseItem)
    {
        var Errors = _itemValidator.Validate(baseItem);
        if (Errors.Any())
            throw new BadRequestException(Errors);

        var computer = _repoComputer.GetById((int)baseItem.ProductId!);
        if (computer is null)
            throw new BadRequestException("productId", "productId invailid!");

        var orderItem = OrderMapper.ToEntity<Computer>(baseItem, computer!);
        var order = _repoOrder.AddItem(orderId, orderItem);
        var response = OrderMapper.ToResponse(order);
        return response;
    }

    public void RemoveItem(int orderId, int itemId)
    {
        _repoOrder.RemoveItem(orderId, itemId);
    }
    public OrderResponse UpdateAddress(UpdatedOrderAddressEntity request)
    {
        var Errors = _addressValidator.Validate(request);
        if (Errors.Any())
            throw new BadRequestException(Errors);

        var updateOrder = _repoOrder.UpdateAddress(request);
        var response = OrderMapper.ToResponse(updateOrder);
        return response;
    }

    public OrderResponse FinalizePayment(int orderID)
    {
        // Verifica compra
        var order = _repoOrder.GetOrder(orderID);
        if (order is null)
            throw new NotFoundException("Order is invalid!");

        if (!order.OrderItems.Any())
            throw new BadRequestException("Order", "Order without items!");

        if (!(order.FinishedDate is null))
            throw new BadRequestException("Order", "Order finished");

        // Integração sistema de pagamento

        // Finalização compra
        order = _repoOrder.FinalizePayment(order);

        // Gera resposta
        var response = OrderMapper.ToResponse(order);
        return response;
    }

    public void Delete(int orderID)
    {
        _repoOrder.Delete(orderID);
    }
}
