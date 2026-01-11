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

namespace ClinicaVeterinaraP1.Pages.Animale
{
    [Authorize(Roles = "Admin,Medic")]
    public class IndexModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public IndexModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (User.IsInRole("Proprietar"))
            {
                var currentEmail = User.Identity.Name;
                Animal = await _context.Animal
                    .Include(a => a.Proprietar)
                    .Where(a => a.Proprietar.Email == currentEmail)
                    .ToListAsync();
            }
            else
            {
                Animal = await _context.Animal
                    .Include(a => a.Proprietar)
                    .ToListAsync();
            }
        }
    }
}