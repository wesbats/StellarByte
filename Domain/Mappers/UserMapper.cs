using Domain.Entities;
using Domain.Request;
using Domain.Requests;

namespace Domain.Mappers;

public class UserMapper
{
    public static User ToEntity(BaseUserRequest user) => new User
    {
        Name = user.Name,
        Email = user.Email,
        PasswordHash = user.PasswordHash,
        Admin = user.Admin
    };
    public static User ToEntity(UpdateUserRequest user) => new User
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        PasswordHash = user.PasswordHash,
        Admin = user.Admin
    };
    public static UserResponse ToResponse(User user) => new UserResponse
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
    };
}
