using AppointmentBooking.DAL.Entity;
using AppointmentBooking.Shared;
using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.BAL.Mappers
{
    public static class Patientmappers
    {
        public static List<DoctorDto> ToDoctorDto(this List<Doctor> doctors)
        {
            return [..doctors.Select(d=>new DoctorDto() {
                Id = d.Id,
                Name = d.Name,
                Specialization = d.Specialization,
                ConsultationFee = d.ConsultationFee,
            })];
        }

        public static List<DoctorDetailsDto> ToDoctorDetailsDto(this List<Doctor> doctors)
        {
            return [..doctors.Select(d=>new DoctorDetailsDto() {
                Id = d.Id,
                Name = d.Name,
                Specialization = d.Specialization,
                ConsultationFee = d.ConsultationFee,
                AvailableDays = d.AvailableDays,
            })];
        }

        public static Appointment ToAppointment(this AppointmentDto appointment)
        {
            return new Appointment()
            {
                PatientId = AppConfig.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                TimeSlot = appointment.AppointmentDate.TimeOfDay.ToString(),
            };
        }

        public static List<AppointmentDto> ToAppintmentsDtos(this List<Appointment> appintments)
        {
            return [..appintments.Select(d=>new AppointmentDto() {
               DoctorId=d.DoctorId,
               AppointmentDate=d.AppointmentDate,
            })];
        }

        public static List<AppointementWithStatus> ToAppointementWiths(this List<Appointment> appointments)
        {
            return [..appointments.Select(d=>new AppointementWithStatus() {
               DoctorId=d.DoctorId,
               AppointmentDate=d.AppointmentDate,
               Status= d.Status,
               Id= d.Id,
            })];
        }

        public static User ToUser(this NewlyPatientDto newlyPatient)
        {
            return new User()
            {
                Email = newlyPatient.Email,
                Password = newlyPatient.Password,
                IsAdmin = false,
            };
        }

        public static Patient ToPatient(this NewlyPatientDto newlyPatient)
        {
            return new Patient()
            {
                Name = newlyPatient.Name,
                PhoneNumber = newlyPatient.PhoneNumber,
                UserId = newlyPatient.UserId,
                Age = newlyPatient.Age,
            };
        }
    }
}
