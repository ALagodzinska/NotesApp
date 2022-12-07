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

namespace NotesApp.Pages.ToDoItems
{
    public class IndexModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public IndexModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        public IList<ToDoItem> ToDoItem { get;set; } = default!;

        public async Task OnGetAsync(int? noteId)
        {
            ViewData["NoteTitle"] = _context.Note.FirstOrDefault(note => note.Id == noteId).Title;
            ViewData["NoteId"] = noteId;

            if (_context.ToDoItem != null)
            {
                ToDoItem = await _context.ToDoItem
                    .Where(x => x.NoteId == noteId)
                    .Include(i => i.Note)
                    .ToListAsync();
            }
        }
    }
}
