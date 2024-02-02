using Domain.Request;
using Domain.Responses;
using System.Collections.Generic;
using System.Reflection;

namespace Domain.Validator;

public class UserValidator : IValidator<BaseUserRequest>
{
    public List<ErrorMessageResponse> Validate(BaseUserRequest user)
    {
        var errors = new List<ErrorMessageResponse>();

        PropertyInfo[] properties = user.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            var value = property.GetValue(user);
            if (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string))
            {
                string propertyName = property.Name != "PasswordHash" ? property.Name : "Password";
                errors.Add(new ErrorMessageResponse
                {
                    Field = propertyName,
                    Message = $"{propertyName} is required!"
                });
            }
        }
        return errors;
    }
}
