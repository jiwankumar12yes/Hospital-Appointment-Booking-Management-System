namespace AppointmentBooking.DAL.Entity
{
    public class Patient:BaseEntity<int>
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public required string   PhoneNumber { get; set; }

        public User User { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
