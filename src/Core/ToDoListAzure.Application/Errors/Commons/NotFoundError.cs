
using FluentResults;

namespace TodoListAzure.Application.Errors.Commons
{
    public class NotFoundError : Error
    {
        public NotFoundError(string className) : base($"{className} not found")
        {
        }

        public NotFoundError(string className, string property, object value) : base($"{className} not found with {property} : {value}")
        {

        }
    }
}
