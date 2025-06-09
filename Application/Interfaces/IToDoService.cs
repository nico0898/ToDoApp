using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetTodosAsync(string userId);
        Task AddTodoAsync(ToDoItem item);
        Task ToggleTodoAsync(int id, string userId);
        Task DeleteTodoAsync(int id, string userId);
    }
}