namespace ClinicManagementApp.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public int AvailableSlots { get; set; }

        public ICollection<Appointment> Appointments { get; set; }  // Navigation property
    }

}
