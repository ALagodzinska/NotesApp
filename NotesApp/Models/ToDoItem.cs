using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Display(Name = "Task")]
        public string Content { get; set; }
        [Display(Name = "Done")]
        public bool IsDone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }
    }
}
