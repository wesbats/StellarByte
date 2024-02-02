using Domain.Entities;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services;

public interface IUserService
{
    List<User> List();
    User? GetById(int id);
    User Create(BaseUserRequest newUser);
    User Update(UpdateUserRequest updatedUser);
    void Delete(int id);
}

public class UserService : IUserService
{
    private readonly IValidator<BaseUserRequest> _validator;
    private readonly IUserRepository _repo;

    public UserService(IValidator<BaseUserRequest> validator, IUserRepository repo)
    {
        _validator = validator;
        _repo = repo;
    }


    public List<User> List()
    {
        return _repo.List();
    }
    public User? GetById(int id)
    {
        var user = _repo.GetById(id);
        return user is null ? null : user;
    }

    public User Create(BaseUserRequest newUser)
    {
        var errors = _validator.Validate(newUser);

        if (errors.Any())
            throw new BadRequestException(errors);

        User user = UserMapper.ToEntity(newUser);
        return _repo.Create(user);
    }

    public User Update(UpdateUserRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var updateUser = UserMapper.ToEntity(request);
        return _repo.Update(updateUser);
    }

    public void Delete(int id)
    {
        _repo.Delete(id);
    }
}
