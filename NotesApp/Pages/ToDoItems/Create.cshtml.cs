using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.ToDoItems
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public CreateModel(NotesApp.Data.NotesAppContext context)
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

            ViewData["NoteId"] = noteId;

            return Page();
        }

        [BindProperty]
        public ToDoItem ToDoItem { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var note = _context.Notes
                .Include(c => c.ToDoList)
                .FirstOrDefault(n => n.Id == ToDoItem.NoteId);

            ToDoItem.PriorityOrder = note.ToDoList.Count + 1;

            ToDoItem.CreationDate = DateTime.Now.Date;
            _context.ToDoItems.Add(ToDoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Notes/Edit", new { id = ToDoItem.NoteId.ToString() });
        }
    }
}
