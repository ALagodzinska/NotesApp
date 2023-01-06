using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NotesApp.Models
{
    public enum Type
    {
        TextNote, ToDoList
    }

    public enum Color
    {
        White,
        Black,
        LightBlue,
        Yellow,
        Red,
        Green,
        Grey,
        Blue,        
        LightGrey
    }

    public class Note
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public string Username { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date of creation")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Note Type")]
        public Type Type { get; set; }

        [Required]
        [Display(Name = "Note Color")]
        public Color Color { get; set; } = Color.White;

        public string ColorClass { get; set; }

        [Display(Name = "List of ToDo Items")]
        public ICollection<ToDoItem> ToDoList { get; set; }

        [Display(Name = "Text")]
        [StringLength(10000, MinimumLength = 1)]
        public string TextContent { get; set; }


        public string GetColorClass(Color color)
        {
            string colorClass = "";

            switch (color)
            {
                case Color.Black:
                    colorClass = "text-white bg-dark";
                    break;
                case Color.Red:
                    colorClass = "text-white bg-danger";
                    break;
                case Color.Blue:
                    colorClass = "text-white bg-primary";
                    break;
                case Color.Green:
                    colorClass = "text-white bg-success";
                    break;
                case Color.Grey:
                    colorClass = "text-white bg-secondary";
                    break;
                case Color.LightBlue:
                    colorClass = "text-dark bg-info";
                    break;
                case Color.Yellow:
                    colorClass = "text-dark bg-warning";
                    break;
                case Color.LightGrey:
                    colorClass = "text-dark bg-light";
                    break;
                case Color.White:
                    colorClass = "";
                    break;
                default:
                    colorClass = "";
                    break;
            }

            return colorClass;
        }
    }
}
