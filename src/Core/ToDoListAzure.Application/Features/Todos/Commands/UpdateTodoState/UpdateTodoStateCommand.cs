using System;

using MediatR;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;

namespace TodoListAzure.Application.Features.Todos.Commands.UpdateTodoState
{
    public class UpdateTodoStateCommand : IRequest<TodoResult>
    {
        public Guid TodoId { get; init; }
        public bool SetToDone { get; init; }
    }
}
