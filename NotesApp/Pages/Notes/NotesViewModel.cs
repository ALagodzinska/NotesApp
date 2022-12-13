using NotesApp.Models;
using NuGet.DependencyResolver;

namespace NotesApp.Pages.Notes
{
    public class NotesViewModel
    {
        public IEnumerable<ToDoNote> ToDoNotes { get; set; }
        public IEnumerable<TextNote> TextNotes { get; set; }
    }
}
