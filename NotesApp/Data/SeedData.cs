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

            var firstNote = new ToDoNote
            {
                Title = "Shop List",
                CreationDate = DateTime.Parse("2022-09-01")
            };
            var secondNote = new ToDoNote
            {
                Title = "Training",
                CreationDate = DateTime.Parse("2022-10-01")
            };
            var thirdNote = new ToDoNote
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
                    PriorityOrder = 3,
                },

                new ToDoItem {
                    Content = "Meat",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstNote,
                    IsDone = false,
                    PriorityOrder= 2,
                },

                new ToDoItem {
                    Content = "Eggs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Five Pushups",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Ten Minutes Run",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                    PriorityOrder = 2,
                },

                new ToDoItem {
                    Content = "Thirty Minutes Yoga",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondNote,
                    IsDone = false,
                    PriorityOrder = 3,
                },

                new ToDoItem {
                    Content = "Fix Bugs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Drink Coffe",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                    PriorityOrder= 2,
                },

                new ToDoItem {
                    Content = "Be Productive",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdNote,
                    IsDone = false,
                    PriorityOrder = 3,
                },
            };

            context.AddRange(toDoItems);
            context.SaveChanges();
        }

    }
}