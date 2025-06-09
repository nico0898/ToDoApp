using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Infrastructure.Services;

public class ToDoService : IToDoService
{
    private readonly ApplicationDbContext _context;

    public ToDoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoItem>> GetTodosAsync(string userId)
    {
        return await _context.TodoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task AddTodoAsync(ToDoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleTodoAsync(int id, string userId)
    {
        var todo = await _context.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (todo != null)
        {
            todo.IsDone = !todo.IsDone;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTodoAsync(int id, string userId)
    {
        var todo = await _context.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (todo != null)
        {
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
