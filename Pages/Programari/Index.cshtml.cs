using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaraP1.Data;
using ClinicaVeterinaraP1.Models;
using Microsoft.AspNetCore.Authorization; 

namespace ClinicaVeterinaraP1.Pages.Programari
{
    [Authorize(Roles = "Admin,Medic,Proprietar")]
    public class IndexModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public IndexModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IList<Programare> Programare { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var currentEmail = User.Identity?.Name;

            if (User.IsInRole("Admin"))
            {
                Programare = await _context.Programare
                    .Include(p => p.Animal)
                        .ThenInclude(a => a.Proprietar)
                    .Include(p => p.MedicVeterinar)
                    .ToListAsync();
            }
            else if (User.IsInRole("Medic"))
            {
                Programare = await _context.Programare
                    .Include(p => p.Animal)
                        .ThenInclude(a => a.Proprietar)
                    .Include(p => p.MedicVeterinar)
                    .Where(p => p.MedicVeterinar.Email == currentEmail)
                    .ToListAsync();
            }
            else if (User.IsInRole("Proprietar"))
            {
                Programare = await _context.Programare
                    .Include(p => p.Animal)
                        .ThenInclude(a => a.Proprietar) 
                    .Include(p => p.MedicVeterinar)
                    .Where(p => p.Animal.Proprietar.Email == currentEmail)
                    .ToListAsync();
            }
            else
            {
                Programare = new List<Programare>();
            }
        }
    }
}