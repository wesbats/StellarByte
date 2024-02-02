using Domain.Entities;
using Domain.Request;

namespace Domain.Mappers;

public class ComputerMapper
{
    public static Computer ToEntity(BaseComputerRequest computer) => new Computer
    {
        Model = computer.Model,
        CPU = computer.CPU,
        Storage = computer.Storage,
        RAM = computer.RAM,
        GPU = computer.GPU,
        Price = computer.Price
    };
    public static Computer ToEntity(UpdateComputerRequest computer) => new Computer
    {
        Id = computer.Id,
        Model = computer.Model,
        CPU = computer.CPU,
        Storage = computer.Storage,
        RAM = computer.RAM,
        GPU = computer.GPU,
        Price = computer.Price
    };
}
