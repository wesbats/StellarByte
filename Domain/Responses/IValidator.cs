using System.Collections.Generic;

namespace Domain.Responses
{
    public interface IValidator<T>
    {
        public List<ErrorMessageResponse> Validate(T obj);
    }
}
