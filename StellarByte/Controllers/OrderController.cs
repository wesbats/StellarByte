using Application.Services;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        var list = _service.ListOrders();
        return Ok(list);
    }

    [HttpGet("{orderId}")]
    public IActionResult Get(int orderId)
    {
        var order = _service.GetOrder(orderId);
        return order is null ? BadRequest() : Ok(order);
    }

    [HttpPost("{orderId}")]
    public IActionResult Post(int orderId, [FromBody] UpdatedOrderAddressRequest request)
    {
        request.OrderId = orderId;
        var order = _service.UpdateAddress(request);
        return Ok(order);
    }

    [HttpDelete("{orderId}")]
    public IActionResult DeleteOrder(int orderId)
    {
        _service.Delete(orderId);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Post([FromBody] BaseOrderRequest request)
    {
        var order = _service.Create(request);
        return Ok(order);
    }

    [HttpGet("{orderId}/items")]
    public IActionResult GetItems(int orderId)
    {
        var order = _service.GetOrderItems(orderId);
        return Ok(order);
    }
    
    [HttpPost("{orderId}/items")]
    public IActionResult AddItem(int orderId, BaseOrderItem requestItem)
    {
        var item = _service.AddItem(orderId, requestItem);
        return Ok(item);
    }

    [HttpGet("{orderId}/items/{itemId}")]
    public IActionResult GetItem(int orderId, int itemId)
    {
        var item = _service.GetOrderItem(orderId, itemId);
        return Ok(item);
    }

    [HttpDelete("{orderId}/items/{itemId}")]
    public IActionResult DeleteItems(int orderId, int itemId)
    {
        var order = _service.RemoveItem(orderId, itemId);
        return Ok(order);
    }

    [HttpGet("{orderId}/Payment")]
    public IActionResult finishPayment(int orderId)
    {
        var order = _service.FinalizePayment(orderId);
        return order.FinishedDate is null ? BadRequest("Falha na finalização do pagamento") : Ok(order);
    }
}
