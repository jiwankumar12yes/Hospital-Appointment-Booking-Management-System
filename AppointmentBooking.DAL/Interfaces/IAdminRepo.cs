using AppointmentBooking.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentBooking.DAL.Interfaces
{
    public interface IAdminRepo
    {
        Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<bool> MarkAsCompletedAsync(int appointmentId);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<List<Appointment>> GetAppointmentsAsync();
    }
}
