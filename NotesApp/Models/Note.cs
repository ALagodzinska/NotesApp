using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NotesApp.Models
{
    public enum Type
    {
        TextNote, ToDoList
    }

    public class Note
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Note Type")]
        public Type Type { get; set; }

        [Display(Name = "List of ToDo Items")]
        public ICollection<ToDoItem> ToDoList { get; set; }

        [Display(Name = "Text")]
        [StringLength(10000, MinimumLength = 1)]
        public string TextContent { get; set; }
    }
}
