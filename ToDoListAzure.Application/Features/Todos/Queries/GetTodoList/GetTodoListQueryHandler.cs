using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TodoListAzure.Application.Features.Commons.Results;
using TodoListAzure.Domain.Entities;
using TodoListAzure.Persistence;

namespace TodoListAzure.Application.Features.Todos.Queries.GetTodoList
{
    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, PageResult<TodoResult>>
    {
        private readonly TodoContext _dbContext;

        public GetTodoListQueryHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageResult<TodoResult>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryId == Guid.Empty)
            {
                throw new Exception();
            }

            var totalItem = await GetTodoListCount(request.CategoryId, cancellationToken);
            var todoList = await GetPagedTodoList(request.CategoryId, request.PaginationOptions, cancellationToken);

            var pagedResult = MapResult(todoList, totalItem, request.PaginationOptions);

            return pagedResult;
        }

        private async Task<int> GetTodoListCount(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Todos
                .Where(t => t.Category.Id.Equals(categoryId))
                .CountAsync(cancellationToken);
        }

        private async Task<List<Todo>> GetPagedTodoList(Guid categoryId, PaginationOptions pageOptions, CancellationToken cancellationToken)
        {
            return await _dbContext.Todos
                .Where(t => t.Category.Id.Equals(categoryId))
                .Skip((pageOptions.Page - 1) * pageOptions.PageSize)
                .Take(pageOptions.PageSize)
                .ToListAsync(cancellationToken);
        }

        private PageResult<TodoResult> MapResult(List<Todo> todoList, int totalItem, PaginationOptions pageOptions)
        {
            var todoListResult = todoList.Select(t => TodoResult.Map(t))
                .ToList();

            return PageResult<TodoResult>.Map(todoListResult,
                totalItem,
                pageOptions.Page,
                pageOptions.PageSize);
        }
    }
}
