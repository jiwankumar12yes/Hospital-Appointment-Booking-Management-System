using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.BAL.Mappers;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using AppointmentBooking.DAL.Repo;
using AppointmentBooking.Shared;
using AppointmentBooking.Shared.Constatnts;
using AppointmentBooking.Shared.Dtos;
using AppointmentBooking.Utility;

namespace AppointmentBooking.BAL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IUserRepo _userRepo;
        public PatientService()
        {
            _patientRepo = new PatientRepo();
            _userRepo = new UserRepo();
        }

        public async Task<List<DoctorDto>?> AvailableDoctorAsync()
        {
            try
            {
                //getting current day 
                WeekDays day = (WeekDays)DateTime.Now.DayOfWeek;
                List<Doctor> doctors = await _patientRepo.AvailableDoctorAsync(day);
                return Patientmappers.ToDoctorDto(doctors);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<DoctorDetailsDto>?> AllDoctorsAsync()
        {
            try
            {
                List<Doctor> doctors = await _patientRepo.GetDoctorsAsync();
                return Patientmappers.ToDoctorDetailsDto(doctors);
            }
            catch
            {
                return null;
            }
        }

        public async Task<(string message, bool success)> AddAppointmentAsync(AppointmentDto appointmentDto)
        {
            try
            {
                Appointment appointment = Patientmappers.ToAppointment(appointmentDto);

                //get previous records to check not book duplicate appoinment 
                List<AppointmentDto> previousAppointment = await GetPatientAppinmtentsAsync();

                //get doctors to check avaible on that date or not
                Doctor? doctor = await _patientRepo.GetDoctorByIdAsync(appointment.DoctorId);

                //returing if doctor not avaible on booking date
                if (!(doctor!.AvailableDays.Contains((WeekDays)appointment.AppointmentDate.DayOfWeek))) return (PatientConstants.DOCTOR_NOT_AVAILABLE, false);

                //checking for duplicate
                bool isAlreadyBooked = false;
                foreach (var app in previousAppointment)
                {
                    if (app.AppointmentDate == appointment.AppointmentDate && app.DoctorId == appointment.DoctorId) isAlreadyBooked = true;
                }

                if (isAlreadyBooked) return (PatientConstants.ALREADY_BOOKED, false);

                //booking appointment
                bool added = await _patientRepo.AddAppointmentAsync(appointment);

                //patient
                User? user = await _patientRepo.GetUserByIdAsync(AppConfig.UserId);

                //sending email  if appointment booked
                if (added)
                {
                    string message = $"Your Appoinment is booked for {appointment.AppointmentDate}  and fee paid {doctor.ConsultationFee} and your doctor is {doctor.Name}";
                    await EmailService.SendEmailAsync(user!.Email, "Appointment Booked", message);
                    return (PatientConstants.APOOITNMENT_DONE, true);
                }

                else return (PatientConstants.APOOITNMENT_FAILED, false);

            }
            catch
            {
                return (UserConstants.CATCH_ERROR, false);
            }
        }

        public async Task<List<AppointmentDto>> GetPatientAppinmtentsAsync()
        {
            List<Appointment> appointments = await _patientRepo.GetUserAppointments(AppConfig.PatientId);
            return Patientmappers.ToAppintmentsDtos(appointments);
        }

        public async Task<List<AppointementWithStatus>?> AppointementWithsAsync()
        {
            try
            {
                List<Appointment> appointments = await _patientRepo.GetUserAppointments(AppConfig.PatientId);
                return Patientmappers.ToAppointementWiths(appointments);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CancelAppoinmentAsync(int appointmentId)
        {
            try
            {
                bool cancled = await _patientRepo.CancelAppoinmentAsync(appointmentId);

                // user for send email
                User? user = await _patientRepo.GetUserByIdAsync(AppConfig.UserId);
                if (cancled)
                {
                    await EmailService.SendEmailAsync(user!.Email, "Appoinment Cancelation", "Your Appoinment is canceled");
                }
                return cancled;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddNewPatientAsync(NewlyPatientDto newlyPatient)
        {
            try
            {
                // adding user before adding patient because relation patient will refere a user 
                User newUser = Patientmappers.ToUser(newlyPatient);
                int newUserId = await _userRepo.AddUserAsync(newUser); // storing userId for give refence in patient

                newlyPatient.UserId = newUserId; // patient is now also a user
                Patient patient = Patientmappers.ToPatient(newlyPatient);
                return await _userRepo.AddPatientAsync(patient); // added patient
            }
            catch
            {
                return false;
            }
        }
    }
}
