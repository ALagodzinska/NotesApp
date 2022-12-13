using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data
{
    public class NotesAppContext : DbContext
    {
        public NotesAppContext (DbContextOptions<NotesAppContext> options)
            : base(options)
        {
        }

        public DbSet<NotesApp.Models.ToDoItem> ToDoItems { get; set; } = default!;

        public DbSet<NotesApp.Models.ToDoNote> ToDoNotes { get; set; }

        public DbSet<NotesApp.Models.TextNote> TextNotes { get; set; }

    }
}
