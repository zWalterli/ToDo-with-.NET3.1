using System;
using Todo.Domain.Enum;

namespace Todo.Domain.ViewModels
{
    public class TodoItemViewModel
    {
        public int? TodoId { get; set; }
        public DateTime? DateInsertion { get; set; }
        public DateTime? DateLastUpdate { get; set; }
        public DateTime? DateConclusion { get; set; }
        public DateTime? Deadline { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public StatusTodoEnum Status { get; set; }
    }
}
