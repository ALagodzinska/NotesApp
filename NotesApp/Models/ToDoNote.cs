using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class ToDoNote
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "List of ToDo Items")]
        public ICollection<ToDoItem> ToDoList { get ; set; }
    }
}
