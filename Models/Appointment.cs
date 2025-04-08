namespace ClinicManagementApp.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Status { get; set; }

        public User User { get; set; }  // Navigation property
        public Doctor Doctor { get; set; }  // Navigation property
    }

}
