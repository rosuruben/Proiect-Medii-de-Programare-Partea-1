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

namespace ClinicaVeterinaraP1.Pages.Proprietari
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context _context;

        public IndexModel(ClinicaVeterinaraP1.Data.ClinicaVeterinaraP1Context context)
        {
            _context = context;
        }

        public IList<Proprietar> Proprietar { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Proprietar = await _context.Proprietar.ToListAsync();
        }
    }
}