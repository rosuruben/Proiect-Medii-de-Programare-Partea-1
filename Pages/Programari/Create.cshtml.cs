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

namespace ClinicaVeterinaraP1.Pages.Programari
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
                ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nume");
            }
            return Page();
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Programare.Animal");
            ModelState.Remove("Programare.MedicVeterinar");
            ModelState.Remove("Programare.Recenzie");

            if (User.IsInRole("Proprietar"))
            {
                var emailProprietar = User.Identity.Name;

                var proprietarDb = await _context.Proprietar
                    .FirstOrDefaultAsync(p => p.Email == emailProprietar);

                if (proprietarDb == null)
                {
                    ModelState.AddModelError("", "Eroare: Nu s-a găsit profilul de proprietar.");
                    ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
                    return Page();
                }

                string numeAnimal = Request.Form["numeAnimalText"];
                string specie = Request.Form["specieText"];
                string rasa = Request.Form["rasaText"];
                string sex = Request.Form["sexText"];
                string microcip = Request.Form["microcipText"];
                string dataNasteriiString = Request.Form["dataNasteriiText"];

                DateTime dataNasterii;
                if (!DateTime.TryParse(dataNasteriiString, out dataNasterii))
                {
                    dataNasterii = DateTime.Now; 
                }

                var animalNou = new Animal
                {
                    ProprietarId = proprietarDb.ProprietarId,
                    Nume = numeAnimal,
                    Specie = specie,
                    Rasa = rasa,
                    Sex = sex,
                    Microcip = microcip,
                    DataNasterii = dataNasterii,
                    Observatii = "Creat la programare"
                };

                _context.Animal.Add(animalNou);
                await _context.SaveChangesAsync(); 

                Programare.AnimalId = animalNou.AnimalId;

                Programare.DataCreare = DateTime.Now;
                Programare.Status = 0; 
                ModelState.Remove("Programare.AnimalId");
            }

            if (!ModelState.IsValid)
            {
                ViewData["MedicVeterinarId"] = new SelectList(_context.MedicVeterinar, "MedicVeterinarId", "Nume");
                if (User.IsInRole("Admin"))
                {
                    ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nume");
                }
                return Page();
            }

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}