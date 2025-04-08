using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementApp.ClinicManagementRazorPages.Pages
{
    public class BookedAppointmentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookedAppointmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointments { get; set; }

        public async Task OnGetAsync()
        {
            var userId = 1;  // Example: Get the logged-in user ID
            Appointments = await _context.Appointments
                .Where(a => a.UserID == userId)
                .Include(a => a.Doctor)
                .ToListAsync();
        }
    }

}
