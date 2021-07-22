
using FluentResults;

namespace TodoListAzure.Domain.Errors.Todos
{
    public class DoStateChangeError : Error
    {
        public DoStateChangeError() 
            : base("A todo in state \"Done\" cannot be  changed to state \"Done\"")
        {
        }
    }
}
