using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;
using TodoListAzure.Domain.Entities;
using TodoListAzure.Persistence;

namespace TodoListAzure.Application.Features.Todos.Commands.CreateTodoList
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoResult>
    {
        private readonly TodoContext _dbContext;

        public CreateTodoCommandHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = Todo.Create(request.Description, DateTime.Now, request.CategoryId);

            var category = await GetCategory(request.CategoryId, cancellationToken);
            if (category == null)
            {
                throw new Exception("404 not found");
            }

            category.AddTodo(todo);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return TodoResult.Map(todo);
        }

        public async Task<TodoCategory> GetCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .SingleOrDefaultAsync(c => c.Id.Equals(categoryId), cancellationToken);
        }
    }
}
