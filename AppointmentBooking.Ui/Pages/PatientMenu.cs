using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.BAL.Services;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.Shared;
using AppointmentBooking.Shared.Constatnts;
using AppointmentBooking.Shared.Dtos;
using ConsoleTables;

namespace AppointmentBooking.Ui.Pages
{
    public class PatientMenu
    {
        private readonly IPatientService _patientService;
        public PatientMenu()
        {
            _patientService = new PatientService();
        }

        public async Task ShowPatientmenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(PatientConstants.AVAILABE_DOC);
                Console.WriteLine(PatientConstants.BOOK_APPOINTMENT);
                Console.WriteLine(PatientConstants.VIEW_APPOINTMENT);
                Console.WriteLine(PatientConstants.CANCEL_APPOINTMENT);
                Console.WriteLine(PatientConstants.LOGOUT);


                Console.WriteLine(UserConstants.ENTR_OPERATION);
                bool isValidInt = int.TryParse(Console.ReadLine(), out int operationNum);

                if (isValidInt)
                {
                    switch (operationNum)
                    {
                        case 1:
                            List<DoctorDto>? doctors = await _patientService.AvailableDoctorAsync();
                            DisplayDoctors(doctors!);
                            break;
                        case 2:
                            await BookAppointmentHandlerAsync();
                            break;
                        case 3:
                            List<AppointementWithStatus>? withStatuses = await _patientService.AppointementWithsAsync();
                            DisplayAppoinments(withStatuses!);
                            break;
                        case 4:
                            List<AppointementWithStatus>? withStatusesCancel = await _patientService.AppointementWithsAsync();
                            DisplayAppoinments(withStatusesCancel!);
                            int appointmentid = TakeId();
                            bool canceled = await _patientService.CancelAppoinmentAsync(appointmentid);
                            if (canceled) Console.WriteLine(PatientConstants.CANCELED);
                            else Console.WriteLine(PatientConstants.CANCEL_FAIL);
                            break;
                        case 5:
                            AppConfig.UserId = default;
                            return;
                        default:
                            Console.WriteLine(UserConstants.INVALID_OPERATION);
                            break;
                    }
                }
                else Console.WriteLine(UserConstants.INVALID_OPERATION_FORMAT);

                Console.WriteLine(UserConstants.PRESS_ANY_KEY);
                Console.ReadKey();
            }
        }

        public static void DisplayAppoinments(List<AppointementWithStatus> appointements)
        {
            if (appointements == null || appointements.Count <= 0) Console.WriteLine(PatientConstants.NO_RECORD_FOUND);
            else
            {
                ConsoleTable table = new("Id", "Date", "Doctor id", "Status");

                foreach (var app in appointements)
                {
                    table.AddRow(app.Id, app.AppointmentDate, app.DoctorId, app.Status);
                }
                table.Write(Format.MarkDown);
            }
        }

        public static void DisplayDoctors(List<DoctorDto> doctors)
        {
            if (doctors == null || doctors.Count <= 0) Console.WriteLine(PatientConstants.NO_RECORD_FOUND);
            else
            {
                ConsoleTable table = new("Id", "Name", "Specialization", "ConsultationFee");

                foreach (var doctor in doctors)
                {
                    table.AddRow(doctor.Id, doctor.Name, doctor.Specialization, doctor.ConsultationFee);
                }
                table.Write(Format.MarkDown);
            }
        }

        public static void DisplayDoctorsDeatils(List<DoctorDetailsDto> doctors)
        {
            if (doctors == null || doctors.Count <= 0) Console.WriteLine(PatientConstants.NO_RECORD_FOUND);
            else
            {
                ConsoleTable table = new("Id", "Name", "Specialization", "ConsultationFee", "Days");
                foreach (var doctor in doctors)
                {
                    string days = string.Join(", ", doctor.AvailableDays); //sepating list into string
                    table.AddRow(doctor.Id, doctor.Name, doctor.Specialization, doctor.ConsultationFee, days);
                }
                table.Write(Format.MarkDown);
            }
        }

        public async Task BookAppointmentHandlerAsync()
        {
            List<DoctorDetailsDto>? doctors = await _patientService.AllDoctorsAsync();
            DisplayDoctorsDeatils(doctors!);

            int doctorId = TakeId();
            DateTime date = TakeAppointmentDate();


            AppointmentDto appointment = new()
            {
                DoctorId = doctorId,
                AppointmentDate = date,
            };

            (string message, bool success) = await _patientService.AddAppointmentAsync(appointment);

            Console.WriteLine(message);
        }

        public static DateTime TakeAppointmentDate()
        {
            bool isValidDate = false;
            DateTime date = default;

            while (!isValidDate)
            {
                Console.WriteLine(PatientConstants.DATE_FORMAT);
                bool isDate = DateTime.TryParse(Console.ReadLine()!, out date);

                if (isDate)
                {
                    if (date >= DateTime.Now) isValidDate = true;
                    else Console.WriteLine(PatientConstants.NOT_PAST);
                }
                else
                {
                    Console.WriteLine(PatientConstants.INVALID_FORMAT);
                }
            }
            return date;
        }

        public static int TakeId()
        {
            bool isVlaidId = false;
            int docId = default;

            while (!isVlaidId)
            {
                Console.WriteLine(PatientConstants.ENTR_ID);
                isVlaidId = int.TryParse(Console.ReadLine()!, out docId);

                if (!isVlaidId) Console.WriteLine(PatientConstants.INVALID_FORMAT);
            }
            return docId;
        }
    }
}
