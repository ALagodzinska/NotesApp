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

namespace NotesApp.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(NotesApp.Data.NotesAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string CurrentFilter { get; set; }
        public PaginatedList<ToDoNote> Notes { get; set; } = default!;

        public async Task OnGetAsync(string searchString, int? pageIndex)
        {
            if (_context.ToDoNotes != null)
            {
                if (searchString != null)
                {
                    pageIndex = 1;
                }

                CurrentFilter = searchString;

                IQueryable<ToDoNote> filteredList = from note in _context.ToDoNotes
                                                 select note;

                if (!String.IsNullOrEmpty(searchString))
                {
                    filteredList = filteredList.Where(n => n.Title.Contains(searchString));
                    filteredList = filteredList.OrderBy(n => n.Title);
                }

                var pageSize = Configuration.GetValue("PageSize", 6);
                Notes = await PaginatedList<ToDoNote>
                    .CreateAsync(
                    filteredList.Include(n => n.ToDoList.OrderBy(n=>n.PriorityOrder)), pageIndex ?? 1, pageSize);
            }
        }
    }
}
