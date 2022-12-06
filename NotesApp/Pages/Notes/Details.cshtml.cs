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
    public class DetailsModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public DetailsModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

      public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            Note = await _context.Note
                .Include(n => n.ToDoList)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Note == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
