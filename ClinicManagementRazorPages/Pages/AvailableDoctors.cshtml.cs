using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementApp.ClinicManagementRazorPages.Pages
{
    public class AvailableDoctorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AvailableDoctorsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctors { get; set; }

        public async Task OnGetAsync()
        {
            Doctors = await _context.Doctors.ToListAsync();
        }
    }
}
