using TodoAPP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPP.Models;


namespace TodoAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, Todo todo)
        {
            
            if (id != todo.Id)
            {
                return BadRequest();
            }

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            todo.Id = id;
            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; 
                }
            }

             
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                // Silinecek öğe bulunamazsa 404 Not Found.
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            // Başarılı silme sonrası içerik döndürmeye gerek yok.
            // Sadece işlemin başarılı olduğunu belirten "204 No Content" durum kodu döndürülür.
            return NoContent();
        }

        // Yardımcı bir metot. Bir todo'nun var olup olmadığını kontrol eder.
        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
