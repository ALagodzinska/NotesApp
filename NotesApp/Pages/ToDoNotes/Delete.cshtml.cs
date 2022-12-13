using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.ToDoNotes
{
    public class DeleteModel : PageModel
    {
        private readonly NotesAppContext _context;

        public DeleteModel(NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoNote Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToDoNotes == null)
            {
                return NotFound();
            }

            var note = await _context.ToDoNotes.FirstOrDefaultAsync(m => m.Id == id);

            if (note == null)
            {
                return NotFound();
            }
            else
            {
                Note = note;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ToDoNotes == null)
            {
                return NotFound();
            }
            var note = await _context.ToDoNotes.FindAsync(id);

            if (note != null)
            {
                Note = note;
                _context.ToDoNotes.Remove(Note);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Notes/Index");
        }
    }
}
