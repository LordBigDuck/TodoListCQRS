using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using FluentResults.Extensions.FluentAssertions;

using TodoListAzure.Domain.Entities;
using TodoListAzure.Domain.Errors.Generics;
using TodoListAzure.Domain.Errors.Todos;

using Xunit;

namespace TodoListAzure.Domain.Tests.Entities
{
    public class TodoTest
    {
        [Fact]
        public void Description_Too_Long_At_Creation()
        {
            var description = new string('a', Todo.DescriptionMaxLength + 1);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;

            var result = Todo.Create(description, date, categoryId);

            result.Should().BeFailure()
                .And.Satisfy(result => {
                    result.Errors.Should()
                        .ContainEquivalentOf(new TextTooLongError(nameof(Todo.Description), Todo.DescriptionMaxLength));
                    });
        }

        [Fact]
        public void Create_Valid_Todo()
        {
            var description = new string('a', Todo.DescriptionMaxLength);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;

            var result = Todo.Create(description, date, categoryId);

            result.Should().BeSuccess();
            var todo = result.Value;
            todo.Description.Should().Be(new string('a', Todo.DescriptionMaxLength));
            todo.IsDone.Should().BeFalse();
            todo.CreationDate.Should().Be(date);
            todo.CategoryId.Should().Be(categoryId);
        }

        [Fact]
        public void Change_Done_State_To_Done_Fails()
        {
            var description = new string('a', Todo.DescriptionMaxLength);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;
            var todoResult = Todo.Create(description, date, categoryId);
            var todo = todoResult.Value;
            todo.SetAsDone();

            var result = todo.SetAsDone();

            result.Should().BeFailure()
                .And.Satisfy(result => {
                    result.Errors.Should()
                        .ContainEquivalentOf(new DoStateChangeError());
                });
        }

        [Fact]
        public void Change_Done_State_To_Undone()
        {
            var description = new string('a', Todo.DescriptionMaxLength);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;
            var todoResult = Todo.Create(description, date, categoryId);
            var todo = todoResult.Value;
            todo.SetAsDone();

            var result = todo.SetAsUndone();

            result.Should().BeSuccess();
            todo.IsDone.Should().BeFalse();
        }

        [Fact]
        public void Change_Undone_State_To_Undone_Fails()
        {
            var description = new string('a', Todo.DescriptionMaxLength);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;
            var todoResult = Todo.Create(description, date, categoryId);
            var todo = todoResult.Value;

            var result = todo.SetAsUndone();

            result.Should().BeFailure()
                .And.Satisfy(result => {
                    result.Errors.Should()
                        .ContainEquivalentOf(new UndoStateChangeError());
                });
        }

        [Fact]
        public void Change_Undone_State_To_Done()
        {
            var description = new string('a', Todo.DescriptionMaxLength);
            var categoryId = Guid.NewGuid();
            var date = DateTime.Now;
            var todoResult = Todo.Create(description, date, categoryId);
            var todo = todoResult.Value;

            var result = todo.SetAsDone();

            result.Should().BeSuccess();
            todo.IsDone.Should().BeTrue();
        }
    }
}
