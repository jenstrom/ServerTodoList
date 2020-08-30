using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTodoList.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var fakeTodos = new Todo[]
            {
                new Todo { Id = Guid.NewGuid(), Text = "Learn about React Ecosystems", IsCompleted = false, CreatedAt = DateTime.Now },
                new Todo { Id = Guid.NewGuid(), Text = "Get together with friends", IsCompleted = false, CreatedAt = DateTime.Now.AddDays(-7) },
                new Todo { Id = Guid.NewGuid(), Text = "Buy groceries", IsCompleted = true, CreatedAt = DateTime.Now.AddDays(-14) }
            };

            modelBuilder.Entity<Todo>().HasData(fakeTodos);
        }
    }
}