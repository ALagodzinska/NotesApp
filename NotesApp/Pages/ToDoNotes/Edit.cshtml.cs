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

namespace NotesApp.Pages.ToDoNotes
{
    public class EditModel : PageModel
    {
        private readonly NotesAppContext _context;

        public EditModel(NotesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoNote Note { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? todoId, int? priorityOrder)
        {
            if (id == null || _context.ToDoNotes == null)
            {
                return NotFound();
            }

            var note = await _context.ToDoNotes
                .Include(c => c.ToDoList.OrderBy(x => x.PriorityOrder))
                .FirstOrDefaultAsync(m => m.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            if (todoId != null && priorityOrder != null)
            {
                var changedOrderItem = note.ToDoList.FirstOrDefault(i => i.Id == todoId);

                note.ToDoList.FirstOrDefault(i => i.PriorityOrder == priorityOrder).PriorityOrder = changedOrderItem.PriorityOrder;
                changedOrderItem.PriorityOrder = priorityOrder.Value;

                Note = note;

                await _context.SaveChangesAsync();

                return RedirectToPage("/ToDoNotes/Edit", new { id });
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

            return RedirectToPage("/Notes/Index");
        }

        private bool NoteExists(int id)
        {
            return _context.ToDoNotes.Any(e => e.Id == id);
        }
    }
}
