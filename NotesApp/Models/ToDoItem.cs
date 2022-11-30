using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsDone { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
