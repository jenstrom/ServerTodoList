using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ServerTodoList.Controllers;
using ServerTodoList.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerTodoListTests.Controllers.TodoControllerTests
{
    public class GetTests
    {
        private TodoContext _todosContext;
        private Todo[] _todos;
        private TodosController _sut;

        [SetUp]
        public void Setup()
        {
            _todosContext = new TodoContext(new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);

            _todos = new Todo[]
            {
                new Todo { Id = Guid.NewGuid(), Text = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, IsCompleted = false },
                new Todo { Id = Guid.NewGuid(), Text = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, IsCompleted = true },
                new Todo { Id = Guid.NewGuid(), Text = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, IsCompleted = false }
            };

            _todosContext.AddRange(_todos);
            _todosContext.SaveChanges();

            _sut = new TodosController(_todosContext);
        }

        [TearDown]
        public void TearDown()
        {
            _todosContext.Dispose();
        }

        [Test]
        public void It_should_return_a_non_empty_list()
        {
            var result = _sut.Get();
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void It_should_return_all_todos_in_context()
        {
            var result = _sut.Get();
            Assert.That(result, Is.EqualTo(_todos));
        }
    }
}
