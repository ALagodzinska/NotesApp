using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NotesApp.Models
{
    public class TextNote
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Display(Name = "Text")]
        [StringLength(10000, MinimumLength = 1)]
        public string TextContent { get; set; }
    }
}
