using System;

namespace TodoListAzure.Domain.Entities
{
    public class Todo
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; } = null;
        public Guid CategoryId { get; private set; }
        public TodoCategory Category { get; private set; } = null;

        public static Todo Create(string description, DateTime creationDate, Guid categoryId)
        {
            if (description.Length > DescriptionMaxLength)
            {
                throw new ArgumentException($"Description cannot be longer than {DescriptionMaxLength} characters");
            }

            return new Todo(description, creationDate, categoryId);
        }

        public void SetAsDone()
        {
            if (IsDone == true)
            {
                throw new Exception();
            }

            IsDone = true;
        }

        public void SetAsUndone()
        {
            if (IsDone == false)
            {
                throw new Exception();
            }

            IsDone = false;
        }

        private const int DescriptionMaxLength = 128;

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
