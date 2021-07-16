using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoListAzure.Domain.Entities
{
    public class TodoCategory
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Todo> TodoList
        {
            get => _todoList.AsReadOnly();
        }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; } = null;

        public static TodoCategory Create(string name, DateTime creationDate)
        {
            return new TodoCategory(name, creationDate);
        }

        public void AddTodo(Todo todo)
        {
            _todoList.Add(todo);
        }

        public void DeleteTodo(Guid todoId)
        {
            _todoList = _todoList
                .Where(t => !t.Id.Equals(todoId))
                .ToList();
        }

        private List<Todo> _todoList = new();

        private TodoCategory() { }

        private TodoCategory(string name, DateTime creationDate)
        {
            Name = name;
            CreationDate = creationDate;
        }
    }
}
