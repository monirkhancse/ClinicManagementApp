using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementApp.ClinicManagementRazorPages.Pages
{
    public class ManageDoctorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageDoctorsModel(ApplicationDbContext context)
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
