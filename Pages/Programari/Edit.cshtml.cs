using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;

namespace ClinicaVeterinaraP1.Pages.Programari
{
    public class EditModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public EditModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programare.FirstOrDefaultAsync(m => m.ProgramareId == id);
            if (programare == null)
            {
                return NotFound();
            }
            Programare = programare;

            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nume");
            ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Programare.Animal");
            ModelState.Remove("Programare.MedicVeterinar");
            ModelState.Remove("Programare.Recenzie");

            if (!ModelState.IsValid)
            {
                ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nume");
                ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
                return Page();
            }

            _context.Attach(Programare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramareExists(Programare.ProgramareId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProgramareExists(int id)
        {
            return _context.Programare.Any(e => e.ProgramareId == id);
        }
    }
}