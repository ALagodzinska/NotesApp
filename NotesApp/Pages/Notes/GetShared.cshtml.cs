using Microsoft.AspNetCore.Authorization;
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
            if(!int.TryParse(shareData, out var noteId))
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
            
            return RedirectToPage("/Notes/Index");
        }
    }
}
