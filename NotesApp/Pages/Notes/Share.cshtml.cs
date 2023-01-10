using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using System.Net;

namespace NotesApp.Pages.Notes
{
    public class ShareModel : PageModel
    {
        private readonly NotesAppContext _context;

        public ShareModel(NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; }

        [BindProperty]
        public string UserEmail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.Include(n => n.ToDoList.OrderBy(n => n.PriorityOrder))
                .FirstOrDefaultAsync(m => m.Id == id);

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
            if (String.IsNullOrEmpty(UserEmail))
            {
                ViewData["EmptyEmail"] = "Text email field is required!";
                return Page();
            }

            if (_context.Users.FirstOrDefault(user => user.Email == UserEmail) == null)
            {
                ViewData["EmailError"] = "Such user doesen't exist!";
                return Page();
            }



            return RedirectToPage("/Notes/Index");
        }

        private static string CreateUniqueShareLink(string shareData)
        {
                return "https://localhost:7278/Notes/GetShared?shareData=" + Uri.EscapeDataString(shareData);    
        }
    }
}
