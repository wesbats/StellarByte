using Domain.Entities;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services;

public interface IComputerService
{
    List<Computer> List();
    Computer? GetById(int id);
    Computer Create(BaseComputerRequest newUser);
    Computer Update(UpdateComputerRequest updatedUser);
    void Delete(int id);
}

public class ComputerService : IComputerService
{
    private readonly IValidator<BaseComputerRequest> _validator;
    private readonly IComputerRepository _repo;

    public ComputerService(IValidator<BaseComputerRequest> validator, IComputerRepository repo)
    {
        _validator = validator;
        _repo = repo;
    }


    public List<Computer> List()
    {
        return _repo.List();
    }

    public Computer? GetById(int id)
    {
        var computer = _repo.GetById(id);
        return computer ?? null;
    }

    public Computer Create(BaseComputerRequest newComputer)
    {
        var errors = _validator.Validate(newComputer);

        if (errors.Any())
            throw new BadRequestException(errors);

        Computer computer = ComputerMapper.ToEntity(newComputer);
        return _repo.Create(computer);
    }

    public Computer Update(UpdateComputerRequest request)
    {

        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var updateComputer = ComputerMapper.ToEntity(request);
        return _repo.Update(updateComputer);
    }

    public void Delete(int id)
    {
        _repo.Delete(id);
    }
}
