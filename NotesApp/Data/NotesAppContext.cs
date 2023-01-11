using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data
{
    public class NotesAppContext : IdentityDbContext
    {
        public NotesAppContext(DbContextOptions<NotesAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedData.SeedUsers(builder);

            base.OnModelCreating(builder);
        }

        public DbSet<ToDoItem> ToDoItems { get; set; } = default!;

        public DbSet<Note> Notes { get; set; } = default!;

        public DbSet<SharedUser> SharedUsers { get; set;} = default!;
    }
}
