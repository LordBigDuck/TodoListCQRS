namespace TodoListAzure.Application.Features.Commons.Results
{
    public class PaginationOptions
    {
        public ushort Page { get; init; } = 1;

        public ushort PageSize { get; init; } = 5;
    }
}
