using System;

using FluentResults;

using MediatR;

using TodoListAzure.Application.Features.Commons.Results;

namespace TodoListAzure.Application.Features.Todos.Queries.GetTodoList
{
    public class GetTodoListQuery : IRequest<Result<PageResult<TodoResult>>>
    {
        public Guid CategoryId { get; init; }
        public PaginationOptions PaginationOptions { get; init; }
    }
}
