using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Context;
using Todo.Data.Repository.Interfaces;
using Todo.Domain.Models;

namespace Todo.Data.Repository.Implementations
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _ctx;

        public TodoRepository(TodoContext context)
        {
            _ctx = context;
        }

        public async Task Delete(int TodoId)
        {
            var todo = await this.GetById(TodoId);
            _ctx.Remove(todo);
            await _ctx.SaveChangesAsync();
        }

        public async Task<TodoItem> GetById(int TodoId)
        {
            return await _ctx.Todos
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TodoId.Equals(TodoId));
        }
        public async Task<List<TodoItem>> GetAll(int page, int pageSize)
        {
            return await _ctx.Todos
                .AsNoTracking()
                .Include(x => x.User)
                .Skip( (page-1) * pageSize )
                .Take( pageSize )
                .ToListAsync();
        }

        public async Task<List<TodoItem>> GetByUserId(int UserId)
        {
            return await _ctx.Todos
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId.Equals(UserId))
                .ToListAsync();
        }

        public async Task<TodoItem> Insert(TodoItem todo)
        {
            await _ctx.AddAsync(todo);
            await _ctx.SaveChangesAsync();
            return todo;
        }

        public async Task<TodoItem> Update(TodoItem todo)
        {
            var todoBD = await _ctx.Todos
                        .FirstOrDefaultAsync(t => t.TodoId.Equals(todo.TodoId));

            _ctx.Entry(todoBD).CurrentValues.SetValues(todo);
            await _ctx.SaveChangesAsync();
            return todo;
        }
    }
}
