using System;

using FluentResults;

using MediatR;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;

namespace TodoListAzure.Application.Features.Todos.Commands.UpdateTodoState
{
    public class UpdateTodoStateCommand : IRequest<Result<TodoResult>>
    {
        public Guid TodoId { get; init; }
        public bool SetToDone { get; init; }
    }
}
