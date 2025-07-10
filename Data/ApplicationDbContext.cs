using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TodoAPP.Models;


namespace TodoAPP.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
    {
    }
    public DbSet<Todo> Todos { get; set; }
    
}