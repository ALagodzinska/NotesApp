using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.Notes
{
    [Authorize]
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
        public Note Note { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            Note.CreationDate = DateTime.Now;
            Note.ColorClass = Note.GetColorClass(Note.Color);
            _context.Notes.Add(Note);

            await _context.SaveChangesAsync();

            if(Note.Type == Models.Type.ToDoList)
            {
                return RedirectToPage("/ToDoItems/Create", new { noteId = Note.Id.ToString() });
            }

            return RedirectToPage("/Notes/TextNoteCreate", new { noteId = Note.Id.ToString() });
            
        }
    }
}
