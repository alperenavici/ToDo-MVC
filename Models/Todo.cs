using System.Runtime.InteropServices.JavaScript;

namespace TodoAPP.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime Created { get; set; } 

}