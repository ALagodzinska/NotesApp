using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using NuGet.Protocol;

namespace NotesApp.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public EditModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoNote Note { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note =  await _context.Note
                .Include(c => c.ToDoList).FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            Note = note;
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

            _context.Attach(Note).State = EntityState.Modified;

            foreach (var todo in Note.ToDoList)
            {
                _context.Attach(todo).State = EntityState.Modified;
            }

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

            return RedirectToPage("/ToDoItems/Index", new { noteId = Note.Id.ToString() });
        }

        private bool NoteExists(int id)
        {
          return _context.Note.Any(e => e.Id == id);
        }
    }
}
