using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.TextNotes
{
    public class CreateModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public CreateModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TextNote TextNote { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TextNote.CreationDate = DateTime.Now.Date;

            _context.TextNotes.Add(TextNote);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Notes/Index");
        }
    }
}
