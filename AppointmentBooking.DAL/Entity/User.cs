namespace AppointmentBooking.DAL.Entity
{
    public class User : BaseEntity<int>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
