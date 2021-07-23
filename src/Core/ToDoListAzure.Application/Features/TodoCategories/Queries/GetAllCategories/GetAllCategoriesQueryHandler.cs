using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

using TodoListAzure.Application.Errors.Commons;
using TodoListAzure.Application.Features.Commons.Results;

using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;
using TodoListAzure.Domain.Entities;
using TodoListAzure.Persistence;

namespace TodoListAzure.Application.Features.TodoCategories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<PageResult<TodoCategoryResult>>>
    {
        private readonly TodoContext _dbContext;

        public GetAllCategoriesQueryHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PageResult<TodoCategoryResult>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var totalItem = await GetTodoCategoryListCount(cancellationToken);
            var todoCategoryList = await GetPagedTodoCategoryList(request.PaginationOptions, cancellationToken);

            var pagedResult = MapResult(todoCategoryList, totalItem, request.PaginationOptions);

            return Result.Ok(pagedResult);
        }

        private async Task<int> GetTodoCategoryListCount(CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .CountAsync(cancellationToken);
        }

        private async Task<List<TodoCategory>> GetPagedTodoCategoryList(PaginationOptions pageOptions, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .Skip((pageOptions.Page - 1) * pageOptions.PageSize)
                .Take(pageOptions.PageSize)
                .ToListAsync(cancellationToken);
        }

        private PageResult<TodoCategoryResult> MapResult(List<TodoCategory> todoCategories, int totalItem, PaginationOptions pageOptions)
        {
            var todoCategoriesResult = todoCategories.Select(t => TodoCategoryResult.Map(t))
                .ToList();

            return PageResult<TodoCategoryResult>.Map(todoCategoriesResult,
                totalItem,
                pageOptions.Page,
                pageOptions.PageSize);
        }
    }
}
