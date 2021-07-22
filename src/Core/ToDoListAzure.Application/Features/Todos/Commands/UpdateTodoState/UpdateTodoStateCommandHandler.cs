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

namespace TodoListAzure.Application.Features.Todos.Commands.UpdateTodoState
{
    public class UpdateTodoStateCommandHandler : IRequestHandler<UpdateTodoStateCommand, Result<TodoResult>>
    {
        private readonly TodoContext _dbContext;

        public UpdateTodoStateCommandHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TodoResult>> Handle(UpdateTodoStateCommand request, CancellationToken cancellationToken)
        {
            var todo = await GetTodo(request.TodoId, cancellationToken);
            if (todo == null)
            {
                return Result.Fail(new NotFoundError(nameof(Todo), nameof(Todo.Id), request.TodoId));
            }

            Result todoResult = request.SetToDone
                ? todo.SetAsDone()
                : todo.SetAsUndone();

            if (todoResult.IsFailed)
            {
                return todoResult;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(TodoResult.Map(todo));
        }

        private async Task<Todo> GetTodo(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Todos
                .SingleOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
        }
    }
}
