using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(NotesAppContext context)
        {
            // Look for any students.
            if (context.Note.Any())
            {
                return;   // DB has been seeded
            }

            var firstNote = new Note
            {
                Title = "Shop List",
                CreationDate = DateTime.Parse("2022-09-01")
            };
            var secondNote = new Note
            {
                Title = "Training",
                CreationDate = DateTime.Parse("2022-10-01")
            };
            var thirdNote = new Note
            {
                Title = "Work Tasks",
                CreationDate = DateTime.Parse("2022-09-06")
            };

            var toDoItems = new ToDoItem[]
            {
                new ToDoItem {
                    Content = "Milk",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Meat",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Eggs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Five Pushups",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Ten Minutes Run",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Thirty Minutes Yoga",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Fix Bugs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Drink Coffe",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                },

                new ToDoItem {
                    Content = "Be Productive",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                },
            };

            context.AddRange(toDoItems);
            context.SaveChanges();
        }

    }
}
