using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.BAL.Interfaces
{
    public interface IAdminService
    {
        Task<bool> MarkCompletedAsync(int AppointId);
        Task<List<AppointementWithStatus>?> AppointmentByDocIdAsync(int docId);
        Task<List<AppointementWithStatus>?> AppointmentBydateAsync(DateTime date);

        Task<List<AppointementWithStatus>?> AllAppointmentsAsync();
    }
}
