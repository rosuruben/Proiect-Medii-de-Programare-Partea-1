using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;

namespace ClinicaVeterinaraP1.Pages.Proprietari
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
            return Page();
        }

        [BindProperty]
        public Proprietar Proprietar { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Proprietar.Add(Proprietar);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
