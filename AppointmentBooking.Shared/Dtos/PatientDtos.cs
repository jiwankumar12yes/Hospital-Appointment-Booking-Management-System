namespace AppointmentBooking.Shared.Dtos
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
    }

    public class DoctorDto : BaseDto<int>
    {
        public required string Name { get; set; }
        public Specialization Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
    }

    public class DoctorDetailsDto : BaseDto<int>
    {
        public required string Name { get; set; }
        public Specialization Specialization { get; set; }
        public decimal ConsultationFee { get; set; }

        public List<WeekDays>? AvailableDays { get; set; }
    }

    public class AppointmentDto
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

    }

    public class AppointementWithStatus : AppointmentDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }

    public class UserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class PatientDto
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public required string PhoneNumber { get; set; }
    }

    public class NewlyPatientDto : UserDto
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public required string PhoneNumber { get; set; }
    }

}
