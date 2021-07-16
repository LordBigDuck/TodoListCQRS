
using System;
using System.Text.Json.Serialization;

using MediatR;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;

namespace TodoListAzure.Application.Features.Todos.Commands.CreateTodoList
{
    public class CreateTodoCommand : IRequest<TodoResult>
    {
        [JsonIgnore]
        public Guid CategoryId { get; private set; } = Guid.Empty;
        public string Description { get; init; }

        public void AddCategoryId(Guid categoryId)
        {
            if (categoryId.Equals(Guid.Empty))
            {
                throw new Exception();
            }
            else if (!CategoryId.Equals(Guid.Empty))
            {
                throw new Exception();
            }

            CategoryId = categoryId;
        }
    }
}
