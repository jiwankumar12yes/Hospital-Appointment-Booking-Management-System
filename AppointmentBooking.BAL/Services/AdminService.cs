using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.BAL.Mappers;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using AppointmentBooking.DAL.Repo;
using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.BAL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        public AdminService()
        {
            _adminRepo = new AdminRepo();
        }

        public async Task<bool> MarkCompletedAsync(int AppointId)
        {
            try
            {
                return await _adminRepo.MarkAsCompletedAsync(AppointId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<AppointementWithStatus>?> AppointmentByDocIdAsync(int docId)
        {
            try
            {
                List<Appointment>? appointment = await _adminRepo.GetAppointmentsByDoctorIdAsync(docId);
                return Patientmappers.ToAppointementWiths(appointment);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AppointementWithStatus>?> AppointmentBydateAsync(DateTime date)
        {
            try
            {
                List<Appointment>? appointment = await _adminRepo.GetAppointmentsByDateAsync(date);
                return Patientmappers.ToAppointementWiths(appointment);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AppointementWithStatus>?> AllAppointmentsAsync()
        {
            try
            {
                List<Appointment>? appointment = await _adminRepo.GetAppointmentsAsync();
                return Patientmappers.ToAppointementWiths(appointment);
            }
            catch
            {
                return null;
            }
        }
    }
}
