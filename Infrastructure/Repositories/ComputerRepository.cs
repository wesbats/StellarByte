using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories;

public interface IComputerRepository
{
    List<Computer> List();
    Computer? GetById(int id);
    bool VerifiById(int id);
    Computer Create(Computer newComputer);
    Computer Update(Computer updatedComputer);
    void Delete(int Id);
}

public class ComputerRepository : IComputerRepository
{
    private List<Computer> _computers { get; set; } = new List<Computer>();
    private int _currentId = 0;
    private int _nextId
    {
        get
        {
            _currentId++;
            return _currentId;
        }
    }

    public List<Computer> List()
    {
        return _computers;
    }

    public Computer? GetById(int id)
    {
        var computer = _computers.FirstOrDefault(x => x.Id == id);
        return computer;
    }

    public bool VerifiById(int id)
    {
        return GetById(id) != null ? true : false;
    }

    public Computer Create(Computer newComputer)
    {
        newComputer.Id = _nextId;
        _computers.Add(newComputer);
        return newComputer;
    }

    public Computer Update(Computer updatedComputer)
    {
        var computer = GetById(updatedComputer.Id);

        if (computer is null)
            throw new Exception("Computer not found!");

        computer.Model = updatedComputer.Model;
        computer.CPU = updatedComputer.CPU;
        computer.Storage = updatedComputer.Storage;
        computer.RAM = updatedComputer.RAM;
        computer.GPU = updatedComputer.GPU;
        computer.Price = updatedComputer.Price;

        return computer;
    }

    public void Delete(int Id)
    {
        var computer = GetById(Id);

        if (computer is null)
            throw new Exception("Computer not found!");

        _computers.Remove(computer);
    }
}
