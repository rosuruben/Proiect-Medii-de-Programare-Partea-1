using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaraP1.Pages.Recenzii
{
    [Authorize(Roles = "Admin,Proprietar")]
    public class CreateModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public CreateModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");

            if (User.IsInRole("Admin"))
            {
                ViewData["ProgramareId"] = new SelectList(_context.Programare, "ProgramareId", "ProgramareId");
                ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");
            }
            return Page();
        }

        [BindProperty]
        public Recenzie Recenzie { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Recenzie.MedicVeterinar");
            ModelState.Remove("Recenzie.Programare");
            ModelState.Remove("Recenzie.Proprietar");

            if (User.IsInRole("Proprietar"))
            {
                var emailProprietar = User.Identity.Name;

                var proprietarDb = await _context.Proprietar.FirstOrDefaultAsync(p => p.Email == emailProprietar);

                if (proprietarDb == null)
                {
                    ModelState.AddModelError("", "Eroare: Nu s-a găsit profilul tău de proprietar.");
                    ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
                    return Page();
                }

                Recenzie.ProprietarId = proprietarDb.ProprietarId;

                Recenzie.DataCreare = DateTime.Now;

                Recenzie.ProgramareId = null;

                ModelState.Remove("Recenzie.ProprietarId");
                ModelState.Remove("Recenzie.ProgramareId");
                ModelState.Remove("Recenzie.DataCreare");
            }

            if (!ModelState.IsValid)
            {
                ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");

                if (User.IsInRole("Admin"))
                {
                    ViewData["ProgramareId"] = new SelectList(_context.Programare, "ProgramareId", "ProgramareId");
                    ViewData["ProprietarId"] = new SelectList(_context.Proprietar, "ProprietarId", "Nume");
                }

                return Page();
            }

            _context.Recenzie.Add(Recenzie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}