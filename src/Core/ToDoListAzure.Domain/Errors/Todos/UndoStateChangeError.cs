
using FluentResults;

namespace TodoListAzure.Domain.Errors.Todos
{
    public class UndoStateChangeError : Error
    {
        public UndoStateChangeError()
            : base("A todo in state \"Undone\" cannot be  changed to state \"Undone\"")
        {
        }
    }
}
