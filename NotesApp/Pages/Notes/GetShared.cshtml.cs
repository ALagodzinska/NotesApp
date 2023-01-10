using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using WebPWrecover.Services;

namespace NotesApp.Pages.Notes
{
    [Authorize]
    public class GetSharedModel : PageModel
    {
        private readonly NotesAppContext _context;

        public GetSharedModel(NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(string shareData)
        {
            if (!int.TryParse(shareData, out var noteId))
            {
                return NotFound();
            }

            var note = await _context.Notes.Include(n => n.ToDoList.OrderBy(n => n.PriorityOrder))
                .FirstOrDefaultAsync(m => m.Id == noteId);

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

        public async Task<IActionResult> OnPostAsync()
        {
            Note = _context.Notes.FirstOrDefault(n => n.Id == Note.Id);

            _context.Attach(Note).State = EntityState.Modified;

            var sharedUser = new SharedUser { UserName = User.Identity.Name, NoteId = Note.Id };

            if (Note.SharedWithUsers.FirstOrDefault(u => u.UserName == User.Identity.Name) == null)
            {
                _context.SharedUsers.Add(sharedUser);
                await _context.SaveChangesAsync();

                Note.SharedWithUsers.Add(sharedUser);
            }
            TempData["AddedShareNote"] = $"{Note.Title} note was added to your note list!";

            return RedirectToPage("/Notes/Index");
        }
    }
}
