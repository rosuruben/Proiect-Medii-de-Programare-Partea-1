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

namespace ClinicaVeterinaraP1.Pages.Recenzii
{
    public class EditModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public EditModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Recenzie Recenzie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzie = await _context.Recenzie.FirstOrDefaultAsync(m => m.RecenzieId == id);
            if (recenzie == null)
            {
                return NotFound();
            }
            Recenzie = recenzie;

            ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
            ViewData["ProgramareId"] = new SelectList(_context.Programare, "ProgramareId", "ProgramareId");
            ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Recenzie.Programare");
            ModelState.Remove("Recenzie.MedicVeterinar");
            ModelState.Remove("Recenzie.Proprietar");

            if (!ModelState.IsValid)
            {
                ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
                ViewData["ProgramareId"] = new SelectList(_context.Programare, "ProgramareId", "ProgramareId");
                ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");

                return Page();
            }

            _context.Attach(Recenzie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecenzieExists(Recenzie.RecenzieId))
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

        private bool RecenzieExists(int id)
        {
            return _context.Recenzie.Any(e => e.RecenzieId == id);
        }
    }
}