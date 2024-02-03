namespace Domain.Request;

public class BaseUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool Admin { get; set; }
}

public class UpdateUserRequest : BaseUserRequest
{
    public int Id { get; set; }
}
