using Application.Services;
using Domain.Exceptions;
using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        List<OrderResponse>? list = _service.ListOrders();
        if (!isAdm)
            list = list.Where(o => o.UserId == userId).ToList();
        return Ok(list);
    }

    [HttpGet("{orderId}")]
    public IActionResult Get(int orderId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        return Ok(order);
    }

    [HttpPost("{orderId}")]
    public IActionResult Post(int orderId, [FromBody] UpdatedOrderAddressRequest request)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        request.OrderId = orderId;
        order = _service.UpdateAddress(request);
        return Ok(order);
    }

    [HttpDelete("{orderId}")]
    public IActionResult DeleteOrder(int orderId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        _service.Delete(orderId);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest request)
    {
        int? userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        BaseOrderRequest formatedRequest = (BaseOrderRequest)request;
        formatedRequest.UserId = userId;
        var order = _service.Create(formatedRequest);
        return Ok(order);
    }

    [HttpGet("{orderId}/items")]
    public IActionResult GetItems(int orderId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        var orderItems = _service.GetOrderItems(orderId);
        return Ok(orderItems);
    }
    
    [HttpPost("{orderId}/items")]
    public IActionResult AddItem(int orderId, BaseOrderItem requestItem)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        var item = _service.AddItem(orderId, requestItem);
        return Ok(item);
    }

    [HttpGet("{orderId}/items/{itemId}")]
    public IActionResult GetItem(int orderId, int itemId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        var item = _service.GetOrderItem(orderId, itemId);
        return Ok(item);
    }

    [HttpDelete("{orderId}/items/{itemId}")]
    public IActionResult DeleteItems(int orderId, int itemId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        var itemDeleted = _service.RemoveItem(orderId, itemId);
        return Ok(itemDeleted);
    }

    [HttpGet("{orderId}/Payment")]
    public IActionResult finishPayment(int orderId)
    {
        var order = _service.GetOrder(orderId);

        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != order!.UserId)
            throw new BadRequestException("UserId", "UserID must be the same as requested");

        var orderToPay = _service.FinalizePayment(orderId);
        return orderToPay.FinishedDate is null ? BadRequest("Falha na finalização do pagamento") : Ok(orderToPay);
    }
}
