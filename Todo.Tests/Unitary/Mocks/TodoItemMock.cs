using System;
using System.Collections.Generic;
using Todo.Domain.Enum;
using Todo.Domain.Models;

namespace Todo.Tests.Unitary.Mocks
{
    public static class TodoItemMock
    {
        public static TodoItem TodoItemOneMock()
            => new TodoItem
            {
                DateInsertion = DateTime.Now.AddDays(-3),
                Deadline = DateTime.Now.AddDays(+3),
                Description = "TESTE",
                Status = StatusTodoEnum.BACKLOG,
                TodoId = 1,
                UserId = 1,
                User = UserMock.UserMock_User()
            };

        public static List<TodoItem> TodoItemListMock()
            => new List<TodoItem>
            {
                new TodoItem
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.BACKLOG,
                    TodoId = 1,
                    UserId = 1,
                    User = UserMock.UserMock_User()
                },
                new TodoItem
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.BACKLOG,
                    TodoId = 1,
                    UserId = 1,
                    User = UserMock.UserMock_User()
                },
                new TodoItem
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.TODO,
                    TodoId = 2,
                    UserId = 1,
                    User = UserMock.UserMock_User()
                }
            };
    }
}
