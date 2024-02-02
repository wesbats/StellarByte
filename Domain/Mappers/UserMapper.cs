using Domain.Entities;
using Domain.Request;

namespace Domain.Mappers;

public class UserMapper
{
    public static User ToEntity(BaseUserRequest user) => new User
    {
        Name = user.Name,
        Mail = user.Mail,
        PasswordHash = user.PasswordHash,
        Admin = user.Admin
    };
    public static User ToEntity(UpdateUserRequest user) => new User
    {
        Id = user.Id,
        Name = user.Name,
        Mail = user.Mail,
        PasswordHash = user.PasswordHash,
        Admin = user.Admin
    };
}
