using Domain.Entities;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    List<User> List();
    User? GetById(int id);
    bool VerifiById(int id);
    User Create(User newUser);
    User Update(User updatedUser);
    void Delete(int Id);
}

public class UserRepository : IUserRepository
{
    private List<User> _users { get; set; } = new List<User>();
    private int _currentId = 0;
    private int _nextId
    {
        get
        {
            _currentId++;
            return _currentId;
        }
    }

    public List<User> List()
    {
        return _users;
    }

    public User? GetById(int id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        return user;
    }

    public bool VerifiById(int id)
    {
        return GetById(id) != null ? true : false;
    }

    public User Create(User newUser)
    {
        newUser.Id = _nextId;
        _users.Add(newUser);
        return newUser;
    }

    public User Update(User updatedUser)
    {
        var user = GetById(updatedUser.Id);

        if (user is null)
            throw new NotFoundException("User not Found!");

        user.Name = updatedUser.Name;
        user.Admin = updatedUser.Admin;
        user.Mail = updatedUser.Mail;
        user.PasswordHash = updatedUser.PasswordHash;

        return user;
    }

    public void Delete(int Id)
    {
        var user = GetById(Id);

        if (user is null)
            throw new NotFoundException("User not found!");

        _users.Remove(user);
    }
}
