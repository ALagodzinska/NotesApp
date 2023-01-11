using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.ToDoItems
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public DeleteModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ToDoItem ToDoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var todoitem = await _context.ToDoItems.FirstOrDefaultAsync(m => m.Id == id);

            if (todoitem == null)
            {
                return NotFound();
            }
            else
            {
                ToDoItem = todoitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }
            var todoitem = await _context.ToDoItems.FindAsync(id);

            if (todoitem != null)
            {
                ToDoItem = todoitem;
                _context.ToDoItems.Remove(ToDoItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Notes/Edit", new { id = todoitem.NoteId.ToString() });
        }
    }
}
