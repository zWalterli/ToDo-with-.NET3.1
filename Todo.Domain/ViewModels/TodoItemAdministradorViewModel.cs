using System;

namespace Todo.Domain.ViewModels
{
    public class TodoItemAdministradorViewModel
    {
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }

    }
}
