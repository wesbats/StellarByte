namespace Domain.Request;

public class CreateOrderRequest
{
    public string? Addres { get; set; }
    public int? CEP { get; set; }
}

public class BaseOrderRequest : CreateOrderRequest
{
    public int? UserId { get; set; }
}

public class UpdatedOrderAddressRequest
{
    public string? Addres { get; set; }
    public int? CEP { get; set; }
}

public class UpdatedOrderAddressEntity
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