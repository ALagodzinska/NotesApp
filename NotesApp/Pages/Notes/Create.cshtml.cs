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
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
