using AppointmentBooking.Shared;

namespace AppointmentBooking.DAL.Entity
{
    public class Appointment : BaseEntity<int>
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public required string TimeSlot { get; set; }
        public Status Status { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
