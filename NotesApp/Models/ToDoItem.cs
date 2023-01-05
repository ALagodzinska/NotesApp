using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Display(Name = "Task")]
        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }

        public int PriorityOrder { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
