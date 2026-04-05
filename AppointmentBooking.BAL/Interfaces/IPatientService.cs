using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.BAL.Interfaces
{
    public interface IPatientService
    {
        Task<List<DoctorDto>?> AvailableDoctorAsync();
        Task<List<DoctorDetailsDto>?> AllDoctorsAsync();
        Task<(string message, bool success)> AddAppointmentAsync(AppointmentDto appointmentDto);
        Task<List<AppointementWithStatus>?> AppointementWithsAsync();
        Task<bool> CancelAppoinmentAsync(int appointmentId);
        Task<bool> AddNewPatientAsync(NewlyPatientDto newlyPatient);
    }
}
