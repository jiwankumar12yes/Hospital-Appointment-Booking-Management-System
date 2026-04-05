using AppointmentBooking.Shared;

namespace AppointmentBooking.DAL.Entity
{
    public class Doctor : BaseEntity<int>
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
        public Specialization Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        public List<WeekDays> AvailableDays { get; set; }

        public User User { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
