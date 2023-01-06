using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data
{
    public static class SeedData
    {
        public static void SeedUsers(this ModelBuilder builder)
        {
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            var appUser = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "admin@notes.com",
                EmailConfirmed = true,
                UserName = "admin@notes.com",
                NormalizedUserName = "ADMIN@NOTES.COM"
            };

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Adminu6ka!");

            //seed user
            builder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public static void Initialize(NotesAppContext context)
        {
            if (context.Notes.Any())
            {
                return;   // DB has been seeded
            }

            var textNotes = new Note[]
            {
                new Note
                {
                    Title = "Eiffel Tower",
                    CreationDate = DateTime.Parse("2022-09-01"),
                    Username = "admin@notes.com",
                    Type = Models.Type.TextNote,
                    Color = Color.Blue,
                    ColorClass = "text-white bg-primary",
                    TextContent = "The Eiffel Tower can be 15 cm taller during the summer, " +
                    "due to thermal expansion meaning the iron heats up, " +
                    "the particles gain kinetic energy and take up more space."
                },

                new Note
                {
                    Title = "Spanish national anthem",
                    CreationDate = DateTime.Parse("2022-09-01"),
                    Username = "admin@notes.com",
                    Type = Models.Type.TextNote,
                    Color = Color.White,
                    ColorClass = "",
                    TextContent = "The Spanish national anthem has no words. " +
                    "The 'Marcha Real' is one of only four national anthems in the world " +
                    "(along with those of Bosnia and Herzegovina, Kosovo, and San Marino) " +
                    "to have no official lyrics"
                },

                new Note
                {
                    Title = "Kit Kat in Japan",
                    CreationDate = DateTime.Parse("2022-09-01"),
                    Username = "admin@notes.com",
                    Type = Models.Type.TextNote,
                    Color = Color.White,
                    ColorClass = "",
                    TextContent = "Japan has over 200 flavours of Kit Kats. " +
                    "They're exclusively created for different regions, cities, and seasons. " +
                    "There are some tasty-sounding ones like banana, blueberry cheesecake and Oreo ice cream, " +
                    "as well as some very questionable ones like baked potato, melon and cheese, wasabi, " +
                    "and vegetable juice."
                }
            };

            var firstToDoNote = new Note
            {
                Title = "Shop List",
                CreationDate = DateTime.Parse("2022-09-01"),
                Username = "admin@notes.com",
                Type = Models.Type.ToDoList,
                Color = Color.Red,
                ColorClass = "text-white bg-danger",
            };

            var secondToDoNote = new Note
            {
                Title = "Training",
                CreationDate = DateTime.Parse("2022-10-01"),
                Username = "admin@notes.com",
                Type = Models.Type.ToDoList,
                Color = Color.Blue,
                ColorClass = "text-white bg-primary",
            };

            var thirdToDoNote = new Note
            {
                Title = "Work Tasks",
                CreationDate = DateTime.Parse("2022-09-06"),
                Username = "admin@notes.com",
                Type = Models.Type.ToDoList,
                Color = Color.White,
                ColorClass = "",
            };

            var toDoItems = new ToDoItem[]
            {
                new ToDoItem {
                    Content = "Milk",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstToDoNote,
                    IsDone = false,
                    PriorityOrder = 3,
                },

                new ToDoItem {
                    Content = "Meat",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstToDoNote,
                    IsDone = false,
                    PriorityOrder= 2,
                },

                new ToDoItem {
                    Content = "Eggs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = firstToDoNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Five Pushups",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondToDoNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Ten Minutes Run",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondToDoNote,
                    IsDone = false,
                    PriorityOrder = 2,
                },

                new ToDoItem {
                    Content = "Thirty Minutes Yoga",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = secondToDoNote,
                    IsDone = false,
                    PriorityOrder = 3,
                },

                new ToDoItem {
                    Content = "Fix Bugs",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdToDoNote,
                    IsDone = false,
                    PriorityOrder = 1,
                },

                new ToDoItem {
                    Content = "Drink Coffe",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdToDoNote,
                    IsDone = false,
                    PriorityOrder= 2,
                },

                new ToDoItem {
                    Content = "Be Productive",
                    CreationDate = DateTime.Parse("2022-09-06"),
                    Note = thirdToDoNote,
                    IsDone = false,
                    PriorityOrder = 3,
                },
            };

            context.AddRange(textNotes);
            context.AddRange(toDoItems);
            context.SaveChanges();
        }

    }
}