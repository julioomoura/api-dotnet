using System.Threading.Tasks;
using MeuTodo.Data;
using MeuTodo.Dtos;
using MeuTodo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route("/v1")]
    public class TodoController : ControllerBase
    {

        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context
            .Todos
            .AsNoTracking()
            .ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        [Route("todos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Date = System.DateTime.Now,
                Done = false,
                Title = dto.Title
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"/v1/todos/{todo.Id}", todo);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }
        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromRoute] int id, [FromBody] UpdateTodoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null) return NotFound();


            try
            {
                todo.Done = dto.Done;
                todo.Title = dto.Title;
                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }
}