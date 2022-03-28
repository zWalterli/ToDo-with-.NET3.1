using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Data.Repository.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetById(int TodoId);
        Task<List<TodoItem>> GetAll(int page, int pageSize);
        Task<List<TodoItem>> GetByUserId(int UserId);
        Task<TodoItem> Insert(TodoItem todo);
        Task<TodoItem> Update(TodoItem todo);
        Task Delete(int TodoId);
    }
}