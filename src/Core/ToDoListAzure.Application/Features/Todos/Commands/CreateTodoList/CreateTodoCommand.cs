
using System;
using System.Text.Json.Serialization;

using FluentResults;

using MediatR;

using TodoListAzure.Application.Errors.Commons;
using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;

namespace TodoListAzure.Application.Features.Todos.Commands.CreateTodoList
{
    public class CreateTodoCommand : IRequest<Result<TodoResult>>
    {
        [JsonIgnore]
        public Guid CategoryId { get; private set; } = Guid.Empty;
        public string Description { get; init; }

        public Result AddCategoryId(Guid categoryId)
        {
            if (categoryId.Equals(Guid.Empty))
            {
                return Result.Fail(new ArgumentError("CategoryId cannot be empty"));
            }
            else if (!CategoryId.Equals(Guid.Empty))
            {
                return Result.Fail(new ArgumentError("CategoryId is already set"));
            }

            CategoryId = categoryId;
            return Result.Ok();
        }
    }
}
