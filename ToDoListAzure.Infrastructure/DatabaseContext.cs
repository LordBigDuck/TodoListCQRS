
using Microsoft.EntityFrameworkCore;

using ToDoListAzure.Core.Domains;

namespace ToDoListAzure.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
