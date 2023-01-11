using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Pages.Notes
{
    [Authorize]
    public class TextNoteCreateModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public TextNoteCreateModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? noteId)
        {
            if (noteId == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(m => m.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            Note = note;

            return Page();
        }

        [BindProperty]
        public Note Note { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (String.IsNullOrEmpty(Note.TextContent) || !ModelState.IsValid)
            {
                ViewData["TextAreaError"] = "Text area field is required!";
                return Page();
            }

            _context.Attach(Note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(Note.Id))
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

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
