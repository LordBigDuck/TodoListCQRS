using System;
using System.Threading;
using System.Threading.Tasks;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TodoListAzure.Application.Errors.Commons;
using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;
using TodoListAzure.Domain.Entities;
using TodoListAzure.Persistence;

namespace TodoListAzure.Application.Features.Todos.Commands.CreateTodoList
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Result<TodoResult>>
    {
        private readonly TodoContext _dbContext;

        public CreateTodoCommandHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TodoResult>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todoResult = Todo.Create(request.Description, DateTime.Now, request.CategoryId);
            if (todoResult.IsFailed)
            {
                return todoResult.ToResult<TodoResult>();
            }

            var category = await GetCategory(request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Result.Fail(new NotFoundError(nameof(TodoCategory), nameof(TodoCategory.Id), request.CategoryId));
            }

            category.AddTodo(todoResult.Value);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(TodoResult.Map(todoResult.Value));
        }

        private async Task<TodoCategory> GetCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .SingleOrDefaultAsync(c => c.Id.Equals(categoryId), cancellationToken);
        }
    }
}
