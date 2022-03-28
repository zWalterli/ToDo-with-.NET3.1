using System;
using System.Collections.Generic;
using Todo.Domain.Enum;
using Todo.Domain.ViewModels;

namespace Todo.Tests.Unitary.Mocks
{
    public static class TodoItemViewModelMock
    {
        public static TodoItemViewModel TodoItemViewModelOneMock()
            => new TodoItemViewModel
            {
                DateInsertion = DateTime.Now.AddDays(-3),
                Deadline = DateTime.Now.AddDays(+3),
                Description = "TESTE",
                Status = StatusTodoEnum.BACKLOG,
                TodoId = 1,
                UserId = 1,
            };

        public static List<TodoItemViewModel> TodoItemViewModelListMock()
            => new List<TodoItemViewModel>
            {
                new TodoItemViewModel
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.BACKLOG,
                    TodoId = 1,
                    UserId = 1,
                },
                new TodoItemViewModel
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.BACKLOG,
                    TodoId = 1,
                    UserId = 1,
                },
                new TodoItemViewModel
                {
                    DateInsertion = DateTime.Now.AddDays(-3),
                    Deadline = DateTime.Now.AddDays(+3),
                    Description = "TESTE",
                    Status = StatusTodoEnum.TODO,
                    TodoId = 2,
                    UserId = 1,
                }
            };
    }
}
