using System;

using TodoListAzure.Domain.Entities;

namespace TodoListAzure.Application.Features.Todos.Queries.GetTodoList
{
    public class TodoResult
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; } = null;

        public static TodoResult Map(Todo todo)
        {
            return new TodoResult
            {
                Id = todo.Id,
                Description = todo.Description,
                IsDone = todo.IsDone,
                CreationDate = todo.CreationDate,
                UpdateDate = todo.UpdateDate
            };
        }
    }
}
