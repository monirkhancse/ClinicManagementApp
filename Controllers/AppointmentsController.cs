using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/appointments/book
        [HttpPost("book")]
        public async Task<ActionResult<Appointment>> BookAppointment([FromBody] Appointment appointment)
        {
            var doctor = await _context.Doctors.FindAsync(appointment.DoctorID);
            if (doctor == null || doctor.AvailableSlots <= 0)
            {
                return BadRequest("No available slots.");
            }

            appointment.Status = "Pending";  // Set initial status as Pending
            _context.Appointments.Add(appointment);
            doctor.AvailableSlots--;  // Reduce available slots for the doctor
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentsByUser), new { userId = appointment.UserID }, appointment);
        }

        // GET: api/appointments/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUser(int userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserID == userId)
                .Include(a => a.Doctor)
                .ToListAsync();

            return Ok(appointments);
        }

        // PUT: api/appointments/cancel/{appointmentId}
        [HttpPut("cancel/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            var currentUserId = 1;  // Example: Get the logged-in user ID (use JWT authentication in production)
            if (appointment.UserID != currentUserId)
            {
                return Unauthorized("You are not authorized to cancel this appointment.");
            }

            appointment.Status = "Cancelled";  // Change status to cancelled
            var doctor = await _context.Doctors.FindAsync(appointment.DoctorID);
            doctor.AvailableSlots++;  // Restore the available slot
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
