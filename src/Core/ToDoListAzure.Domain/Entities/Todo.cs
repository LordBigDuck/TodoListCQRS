using System;

using FluentResults;

using TodoListAzure.Domain.Errors.Generics;
using TodoListAzure.Domain.Errors.Todos;

namespace TodoListAzure.Domain.Entities
{
    public class Todo
    {
        public const int DescriptionMaxLength = 128;

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; } = null;
        public Guid CategoryId { get; private set; }
        public TodoCategory Category { get; private set; } = null;

        public static Result<Todo> Create(string description, DateTime creationDate, Guid categoryId)
        {
            if (description.Length > DescriptionMaxLength)
            {
                return Result.Fail(new TextTooLongError(nameof(Description), DescriptionMaxLength));
            }

            return Result.Ok(new Todo(description, creationDate, categoryId));
        }

        public Result SetAsDone()
        {
            if (IsDone == true)
            {
                return Result.Fail(new DoStateChangeError());
            }

            IsDone = true;
            return Result.Ok();
        }

        public Result SetAsUndone()
        {
            if (IsDone == false)
            {
                return Result.Fail(new UndoStateChangeError());
            }

            IsDone = false;
            return Result.Ok();
        }

        

        private Todo() { }

        private Todo(string description, DateTime creationDate, Guid categoryId)
        {
            Description = description;
            CreationDate = creationDate;
            IsDone = false;
            CategoryId = categoryId;
        }
    }
}
