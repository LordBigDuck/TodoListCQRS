
using FluentResults;

using MediatR;

using TodoListAzure.Application.Features.Commons.Results;

namespace TodoListAzure.Application.Features.TodoCategories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<Result<PageResult<TodoCategoryResult>>>
    {
        public PaginationOptions PaginationOptions { get; init; }
    }
}
