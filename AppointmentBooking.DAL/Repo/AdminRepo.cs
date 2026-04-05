using AppointmentBooking.DAL.Context;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.DAL.Repo
{
    public class AdminRepo : IAdminRepo
    {
        private readonly DBContext _dbContext;
        public AdminRepo()
        {
            _dbContext = new DBContext();
        }

        public async Task<bool> MarkAsCompletedAsync(int appointmentId)
        {
            Appointment? appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null) return false;
            appointment.Status = Shared.Status.Completed;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _dbContext.Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _dbContext.Appointments.Where(a => a.AppointmentDate.Date == (date.Date)).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _dbContext.Appointments.ToListAsync();
        }
    }
}
