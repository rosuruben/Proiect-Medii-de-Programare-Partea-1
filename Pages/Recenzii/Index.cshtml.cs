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

namespace ClinicaVeterinaraP1.Pages.Recenzii
{
    [Authorize(Roles = "Admin,Medic,Proprietar")]
    public class IndexModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public IndexModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IList<Recenzie> Recenzie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var currentEmail = User.Identity.Name;

            if (User.IsInRole("Medic"))
            {
                
                Recenzie = await _context.Recenzie
                    .Include(r => r.MedicVeterinar)
                    .Include(r => r.Programare)
                    .Include(r => r.Proprietar)
                    .Where(r => r.MedicVeterinar.Email == currentEmail) 
                    .ToListAsync();
            }
            else
            {
            
                Recenzie = await _context.Recenzie
                    .Include(r => r.MedicVeterinar)
                    .Include(r => r.Programare)
                    .Include(r => r.Proprietar)
                    .ToListAsync();
            }
        }
    }
}