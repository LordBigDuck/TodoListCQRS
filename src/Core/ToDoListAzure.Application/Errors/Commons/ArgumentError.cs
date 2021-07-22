
using FluentResults;

namespace TodoListAzure.Application.Errors.Commons
{
    public class ArgumentError : Error
    {
        public ArgumentError(string errorMessage) : base(errorMessage) { }
    }
}
