using System.Collections.Generic;

namespace TodoListAzure.Application.Features.Commons.Results
{
    public class PageResult<T>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalItems { get; init; }
        public ICollection<T> Items { get; init; }

        public static PageResult<T> Map(ICollection<T> items, int totalItems, int page, int pageSize)
        {
            return new PageResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
