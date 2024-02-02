using Domain.Request;
using Domain.Responses;
using System.Collections.Generic;
using System.Reflection;

namespace Domain.Validator;

public class OrderValidator : IValidator<BaseOrderRequest>
{
    public List<ErrorMessageResponse> Validate(BaseOrderRequest order)
    {
        var errors = new List<ErrorMessageResponse>();

        PropertyInfo[] properties = order.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            var value = property.GetValue(order);
            if (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string))
                errors.Add(new ErrorMessageResponse
                {
                    Field = property.Name,
                    Message = $"{property.Name} is required!"
                });
            if (property.PropertyType == typeof(int?) && (int?)value! <= 0)
                errors.Add(new ErrorMessageResponse
                {
                    Field = property.Name,
                    Message = $"{property.Name} is not a valid positive integer!"
                });
        }
        return errors;
    }
}