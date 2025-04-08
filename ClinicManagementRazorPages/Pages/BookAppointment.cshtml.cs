using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementApp.ClinicManagementRazorPages.Pages
{
    public class BookAppointmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookAppointmentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public List<SelectListItem> DoctorsSelectList { get; set; }

        public async Task OnGetAsync()
        {
            DoctorsSelectList = await _context.Doctors
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorID.ToString(),
                    Text = $"{d.Name} - {d.Specialization}"
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var doctor = await _context.Doctors.FindAsync(Appointment.DoctorID);
            if (doctor.AvailableSlots <= 0)
            {
                ModelState.AddModelError("", "No available slots.");
                return Page();
            }

            Appointment.Status = "Pending";  // Initially, the status is Pending
            _context.Appointments.Add(Appointment);
            doctor.AvailableSlots--; // Reduce available slots
            await _context.SaveChangesAsync();

            return RedirectToPage("/Appointments/BookedAppointments");
        }
    }

}
