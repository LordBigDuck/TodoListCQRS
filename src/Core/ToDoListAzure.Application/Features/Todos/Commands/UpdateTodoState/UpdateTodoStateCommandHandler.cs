using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;
using TodoListAzure.Domain.Entities;
using TodoListAzure.Persistence;

namespace TodoListAzure.Application.Features.Todos.Commands.UpdateTodoState
{
    public class UpdateTodoStateCommandHandler : IRequestHandler<UpdateTodoStateCommand, TodoResult>
    {
        private readonly TodoContext _dbContext;

        public UpdateTodoStateCommandHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoResult> Handle(UpdateTodoStateCommand request, CancellationToken cancellationToken)
        {
            var todo = await GetTodo(request.TodoId, cancellationToken);
            if (todo == null)
            {
                throw new Exception("not found");
            }

            if (request.SetToDone)
            {
                todo.SetAsDone();
            }
            else
            {
                todo.SetAsUndone();
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return TodoResult.Map(todo);
        }

        private async Task<Todo> GetTodo(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Todos
                .SingleOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
        }
    }
}
