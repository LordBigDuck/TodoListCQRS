using System.Collections.Generic;

namespace TodoListAzure.Api.Models
{
    public class ErrorResponse
    {
        public ICollection<string> Errors { get; set; } = new List<string>();
        public string StackTrace { get; set; } = string.Empty;
    }
}
