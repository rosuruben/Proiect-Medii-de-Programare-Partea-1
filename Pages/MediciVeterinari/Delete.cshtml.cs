using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;

namespace ClinicaVeterinaraP1.Pages.MediciVeterinari
{
    public class DeleteModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public DeleteModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
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

            var medicveterinar = await _context.MedicVeterinar.FirstOrDefaultAsync(m => m.MedicVeterinarId == id);

            if (medicveterinar == null)
            {
                return NotFound();
            }
            else
            {
                MedicVeterinar = medicveterinar;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicveterinar = await _context.MedicVeterinar.FindAsync(id);
            if (medicveterinar != null)
            {
                MedicVeterinar = medicveterinar;
                _context.MedicVeterinar.Remove(MedicVeterinar);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
