using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Domain.Enum;

namespace Todo.Domain.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodoId { get; set; }
        public DateTime? DateInsertion { get; set; }
        public DateTime? DateLastUpdate { get; set; }
        public DateTime? DateConclusion { get; set; }
        public DateTime? Deadline { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public StatusTodoEnum Status { get; set; }
        public User User { get; set; }


        // Methods
        public void GenerateDateInsertion()
            => this.DateInsertion = DateTime.Now;

        public void GenerateDateConclusion()
            => this.DateConclusion = DateTime.Now;

        public void GenerateDateLastUpdate()
            => this.DateLastUpdate = DateTime.Now;

        public bool isValid
        {
            get
            {
                return !(UserId < 1) && !string.IsNullOrEmpty(Description) && !(Deadline == null);
            }
        }
    }
}
