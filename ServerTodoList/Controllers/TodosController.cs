using Microsoft.AspNetCore.Mvc;
using ServerTodoList.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return _context.Todos.ToList();
        }

        public class NewTodo
        {
            public string Text { get; set; }
        }

        [HttpPost]
        public Todo Post(NewTodo newTodo)
        {
            var todo = new Todo
            {
                Text = newTodo.Text,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return todo;
        }
        [HttpDelete("{id}")]
        public ActionResult<Todo> Delete(Guid id)
        {
            Todo todoToDelete;
            try
            {
                todoToDelete = _context.Todos.First(t => t.Id == id);
            }
            catch (InvalidOperationException)
            {
                return NotFound(id);
            }
            _context.Todos.Remove(todoToDelete);
            _context.SaveChanges();
            return todoToDelete;
        }

        [HttpPost("{id}/completed")]
        public ActionResult<Todo> Post(Guid id)
        {
            Todo completedTodo;
            try
            {
                completedTodo = _context.Todos.First(t => t.Id == id);
            }
            catch (InvalidOperationException)
            {
                return NotFound(id);
            }
            completedTodo.IsCompleted = true;
            _context.SaveChanges();
            return completedTodo;
        }
    }
}
