using Domain.Request;
using Domain.Responses;
using System.Collections.Generic;
using System.Reflection;

namespace Domain.Validator;

public class ComputerValidator : IValidator<BaseComputerRequest>
{
    public List<ErrorMessageResponse> Validate(BaseComputerRequest computer)
    {
        var errors = new List<ErrorMessageResponse>();

        PropertyInfo[] properties = computer.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            var value = property.GetValue(computer);
            if (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string))
                errors.Add(new ErrorMessageResponse
                {
                    Field = property.Name,
                    Message = $"{property.Name} is required!"
                });
            if (property.PropertyType == typeof(int?) && (int?)value! <= 0 ||
                property.PropertyType == typeof(decimal) && (decimal?)value! <= 0)
                errors.Add(new ErrorMessageResponse
                {
                    Field = property.Name,
                    Message = $"{property.Name} is not a valid positive integer!"
                });
        }
        return errors;
    }
}
