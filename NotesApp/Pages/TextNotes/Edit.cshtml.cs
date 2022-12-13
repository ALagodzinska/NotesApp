using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.TextNotes
{
    public class EditModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public EditModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TextNote TextNote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TextNotes == null)
            {
                return NotFound();
            }

            var textnote =  await _context.TextNotes.FirstOrDefaultAsync(m => m.Id == id);
            if (textnote == null)
            {
                return NotFound();
            }
            TextNote = textnote;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TextNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextNoteExists(TextNote.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Notes/Index");
        }

        private bool TextNoteExists(int id)
        {
          return _context.TextNotes.Any(e => e.Id == id);
        }
    }
}
