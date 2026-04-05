using AppointmentBooking.DAL.Entity;

namespace AppointmentBooking.DAL.Interfaces
{
    public interface IUserRepo
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddPatientAsync(Patient patient);
        Task<int> AddUserAsync(User user);
    }
}
