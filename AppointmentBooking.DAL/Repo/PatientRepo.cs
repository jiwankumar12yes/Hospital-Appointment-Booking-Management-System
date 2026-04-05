using AppointmentBooking.DAL.Context;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using AppointmentBooking.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.DAL.Repo
{
    public class PatientRepo : IPatientRepo
    {
        private readonly DBContext _dbContext;
        public PatientRepo()
        {
            _dbContext = new DBContext();
        }

        public async Task<List<Doctor>> AvailableDoctorAsync(WeekDays day)
        {
            return await _dbContext.Doctors.Where(d => d.AvailableDays.Contains(day)).ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _dbContext.Doctors.Include(d => d.Appointments).ToListAsync();
        }

        public async Task<bool> AddAppointmentAsync(Appointment appointment)
        {
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Appointment>> GetUserAppointments(int patId)
        {
            return await _dbContext.Appointments.AsNoTracking().Where(a => a.PatientId == patId).ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> CancelAppoinmentAsync(int appointmentId)
        {
            Appointment? appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment!.AppointmentDate.Date <= DateTime.Now.Date) return false;

            //additional (can cancel own appointment)
            if (appointment.PatientId != AppConfig.PatientId) return false;

            appointment!.Status = Status.Cancelled;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
