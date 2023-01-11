namespace NotesApp.Models
{
    public class SharedUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
