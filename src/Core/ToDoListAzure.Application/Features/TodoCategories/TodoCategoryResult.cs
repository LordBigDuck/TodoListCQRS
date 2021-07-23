using System;
using System.Collections.Generic;

using TodoListAzure.Domain.Entities;

namespace TodoListAzure.Application.Features.TodoCategories
{
    public class TodoCategoryResult
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Todo> TodoList { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; } = null;

        public static TodoCategoryResult Map(TodoCategory category)
        {
            return new TodoCategoryResult
            {
                Id = category.Id,
                Name = category.Name,
                TodoList = category.TodoList,
                CreationDate = category.CreationDate,
                UpdateDate = category.UpdateDate
            };
        }
    }
}
