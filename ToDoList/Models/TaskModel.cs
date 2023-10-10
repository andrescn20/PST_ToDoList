using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        [StringLength(250)]
        public string Description { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

    }
}
