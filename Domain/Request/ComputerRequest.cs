namespace Domain.Request;

public class BaseComputerRequest
{
    public string? Model { get; set; }
    public string? CPU { get; set; }
    public int? Storage { get; set; }
    public int? RAM { get; set; }
    public string? GPU { get; set; }
    public decimal Price { get; set; }
}

public class UpdateComputerRequest : BaseComputerRequest
{
    public int Id { get; set; }
}
