namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Mail { get; set; }
    public string? PasswordHash { get; set; }
    public bool Admin { get; set; }
}
