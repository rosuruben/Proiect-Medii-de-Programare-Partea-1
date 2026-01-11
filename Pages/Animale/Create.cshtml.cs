using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;

namespace ClinicaVeterinaraP1.Pages.Animale
{
    public class CreateModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public CreateModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");
            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Animal.Proprietar");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");
                return Page();
            }

            _context.Animal.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
