using AppointmentBooking.DAL.Entity;
using AppointmentBooking.Shared;

namespace AppointmentBooking.DAL.Interfaces
{
    public interface IPatientRepo
    {
        Task<List<Doctor>> GetDoctorsAsync();
        Task<List<Doctor>> AvailableDoctorAsync(WeekDays day);
        Task<bool> AddAppointmentAsync(Appointment appointment);
        Task<List<Appointment>> GetUserAppointments(int patId);
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<bool> CancelAppoinmentAsync(int appointmentId);
        Task<User?> GetUserByIdAsync(int id);
    }
}
