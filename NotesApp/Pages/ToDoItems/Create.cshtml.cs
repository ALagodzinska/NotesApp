﻿using System;
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
    public class CreateModel : PageModel
    {
        private readonly NotesApp.Data.NotesAppContext _context;

        public CreateModel(NotesApp.Data.NotesAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? noteId)
        {
            if (noteId == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FirstOrDefaultAsync(m => m.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            Note = note;
            return Page();
        }

        public Note Note { get; set; }

        [BindProperty]
        public ToDoItem ToDoItem { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            ToDoItem.Note = Note;
            ToDoItem.CreationDate = DateTime.Now.Date;
            _context.ToDoItem.Add(ToDoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
