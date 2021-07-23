using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TodoListAzure.Domain.Entities;

namespace TodoListAzure.Persistence.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        private const string TableName = "Todo";
        private const string PublicTodoListName = "TodoList";
        private const string PrivateTodoListName = "_todoList";

        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable(TableName);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(c => c.Id);

            builder.HasOne(t => t.Category)
                .WithMany(c => c.TodoList)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
