using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.ViewModels;

namespace Todo.Service.Interfaces.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoItemViewModel>> GetByUser(string email);
        Task<List<TodoItemAdministradorViewModel>> GetAll(string email, int page, int pageSize, bool onlyLate);
        Task<TodoItemViewModel> GetById(int todoId);
        Task<TodoItemViewModel> Insert(TodoItemViewModel todo);
        Task<TodoItemViewModel> Update(TodoItemViewModel todo);
        Task Delete(int TodoId);
    }
}