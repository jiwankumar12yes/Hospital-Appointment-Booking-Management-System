namespace AppointmentBooking.BAL.Interfaces
{
    public interface IUserService
    {
        Task<(string message, bool success, bool isAdmin)> LoginUserAsync(string email, string password);
    }
}
