using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TodoListAzure.Domain.Entities;

namespace TodoListAzure.Persistence.Configurations
{
    public class TodoCategoryConfiguration : IEntityTypeConfiguration<TodoCategory>
    {
        private const string TableName = "Category";

        public void Configure(EntityTypeBuilder<TodoCategory> builder)
        {
            builder.ToTable(TableName);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(c => c.Id);
        }
    }
}
