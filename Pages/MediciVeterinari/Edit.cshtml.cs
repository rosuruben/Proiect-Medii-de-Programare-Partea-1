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

namespace ClinicaVeterinaraP1.Pages.MediciVeterinari
{
    public class EditModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public EditModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicVeterinar MedicVeterinar { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicveterinar =  await _context.MedicVeterinar.FirstOrDefaultAsync(m => m.MedicVeterinarId == id);
            if (medicveterinar == null)
            {
                return NotFound();
            }
            MedicVeterinar = medicveterinar;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MedicVeterinar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicVeterinarExists(MedicVeterinar.MedicVeterinarId))
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

        private bool MedicVeterinarExists(int id)
        {
            return _context.MedicVeterinar.Any(e => e.MedicVeterinarId == id);
        }
    }
}
