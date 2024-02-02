namespace Domain.Request;

public class BaseOrderRequest
{
    public int? UserId { get; set; }
    public string? Addres { get; set; }
    public int? CEP { get; set; }
}

public class UpdatedOrderAddressRequest
{
    public int? OrderId { get; set; }
    public string? Addres { get; set; }
    public int? CEP { get; set; }
}

public class BaseOrderItem
{
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
}