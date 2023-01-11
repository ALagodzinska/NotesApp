using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace NotesApp.Pages.Notes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(NotesApp.Data.NotesAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public string TypeSort { get; set; }
        public string SharedSort { get; set; }
        public PaginatedList<Note> Notes { get; set; } = default!;

        public async Task OnGetAsync(string searchString, string sortOrder, int? pageIndex)
        {
            if (_context.Notes != null)
            {
                CurrentSort = sortOrder;               

                if (searchString != null)
                {
                    pageIndex = 1;
                }

                CurrentFilter = searchString;

                TypeSort = sortOrder == "todo_type" ? "text_type" : "todo_type";
                SharedSort = sortOrder == "sharedNotes" ? "myNotes" : "sharedNotes";

                IQueryable<Note> filteredList = from note in _context.Notes
                                                where note.Username == User.Identity.Name 
                                                || note.SharedWithUsers.Contains(_context.SharedUsers
                                                .FirstOrDefault(s => s.UserName == User.Identity.Name && s.NoteId == note.Id))
                                                select note;

                if (!String.IsNullOrEmpty(searchString))
                {
                    filteredList = filteredList.Where(n => n.Title.Contains(searchString));
                    filteredList = filteredList.OrderBy(n => n.Title);
                }

                if(sortOrder == "text_type")
                {
                    filteredList = filteredList.Where(n => n.Type == Models.Type.TextNote);
                }
                else if (sortOrder == "todo_type")
                {
                    filteredList = filteredList.Where(n => n.Type == Models.Type.ToDoList);
                }
                else if(sortOrder == "myNotes")
                {
                    filteredList = filteredList.Where(n => n.Username == User.Identity.Name);
                }
                else if (sortOrder == "sharedNotes")
                {
                    filteredList = filteredList.Where(note => note.SharedWithUsers.Contains(_context.SharedUsers
                                                .FirstOrDefault(s => s.UserName == User.Identity.Name && s.NoteId == note.Id)));
                }

                var pageSize = Configuration.GetValue("PageSize", 6);
                Notes = await PaginatedList<Note>
                    .CreateAsync(
                    filteredList.Include(n => n.ToDoList.OrderBy(n => n.PriorityOrder)).OrderByDescending(n=> n.CreationDate)
                    , pageIndex ?? 1, pageSize);
            }
        }
    }
}
