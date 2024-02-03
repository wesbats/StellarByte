using Domain.Entities;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Request;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services;

public interface IUserService
{
    List<UserResponse> List();
    UserResponse? GetById(int id);
    UserResponse Create(BaseUserRequest newUser);
    UserResponse Update(UpdateUserRequest updatedUser);
    void Delete(int id);
}

public class UserService : IUserService
{
    private readonly IValidator<BaseUserRequest> _validator;
    private readonly IUserRepository _repo;
    private readonly IHashingService _hashingService;

    public UserService(IValidator<BaseUserRequest> validator, IUserRepository repo, IHashingService hashingService)
    {
        _validator = validator;
        _repo = repo;
        _hashingService = hashingService;
    }


    public List<UserResponse> List()
    {
        var users = _repo.List();
        var response = users.Select(user => UserMapper.ToResponse(user)).ToList();
        return response;
    }
    public UserResponse? GetById(int id)
    {
        var user = _repo.GetById(id);
        return user is null ? null : UserMapper.ToResponse(user);
    }

    public UserResponse Create(BaseUserRequest newUser)
    {
        var errors = _validator.Validate(newUser);
        if (errors.Any())
            throw new BadRequestException(errors);

        User user = UserMapper.ToEntity(newUser);
        user.PasswordHash = _hashingService.Hash(user.PasswordHash!);
        return UserMapper.ToResponse(_repo.Create(user));
    }

    public UserResponse Update(UpdateUserRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Any())
            throw new BadRequestException(errors);
        var exist = _repo.GetById(request.Id) != null;
        if (!exist)
            throw new NotFoundException("User not found!");

        var updateUser = UserMapper.ToEntity(request);
        updateUser.PasswordHash = _hashingService.Hash(updateUser.PasswordHash!);
        return UserMapper.ToResponse(_repo.Update(updateUser));
    }

    public void Delete(int id)
    {
        _repo.Delete(id);
    }
}
