using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.TextNotes
{
    public class DeleteModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public DeleteModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TextNote TextNote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TextNotes == null)
            {
                return NotFound();
            }

            var textnote = await _context.TextNotes.FirstOrDefaultAsync(m => m.Id == id);

            if (textnote == null)
            {
                return NotFound();
            }
            else 
            {
                TextNote = textnote;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TextNotes == null)
            {
                return NotFound();
            }
            var textnote = await _context.TextNotes.FindAsync(id);

            if (textnote != null)
            {
                TextNote = textnote;
                _context.TextNotes.Remove(TextNote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
