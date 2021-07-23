
using Microsoft.EntityFrameworkCore;

using TodoListAzure.Domain.Entities;

namespace TodoListAzure.Persistence
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoCategory> Categories { get; set; }
    }
}
