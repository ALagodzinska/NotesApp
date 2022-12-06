using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public IndexModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        public IList<Note> Notes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Note != null)
            {
                Notes = await _context.Note.
                    Include(i => i.ToDoList).
                    ToListAsync();
            }
        }
    }
}
