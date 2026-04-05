using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.BAL.Services;
using AppointmentBooking.Shared;
using AppointmentBooking.Shared.Constatnts;
using AppointmentBooking.Shared.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBooking.Ui.Pages
{
    public class AdminMenu
    {
        private readonly IPatientService _patientService;
        private readonly IAdminService _adminService;
        public AdminMenu()
        {
            _patientService = new PatientService();
            _adminService = new AdminService();
        }
        public async Task ShowAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(AdminConstants.MARK);
                Console.WriteLine(AdminConstants.ALL_APPOINTMENT);
                Console.WriteLine(AdminConstants.ALL_DOCTOR);
                Console.WriteLine(AdminConstants.ADMIN_LOGOUT);

                Console.WriteLine(UserConstants.ENTR_OPERATION);
                bool isValidInt = int.TryParse(Console.ReadLine(), out int operationNum);

                if (isValidInt)
                {
                    switch (operationNum)
                    {
                        case 1:
                            List<AppointementWithStatus>? appointments = await _adminService.AllAppointmentsAsync();
                            PatientMenu.DisplayAppoinments(appointments!);
                            int appointmentid = PatientMenu.TakeId();
                            bool compeleted = await _adminService.MarkCompletedAsync(appointmentid);
                            if (compeleted) Console.WriteLine(AdminConstants.MARK_COMPLETED);
                            else Console.WriteLine(AdminConstants.MARK_COMPLETED_FAIL);
                            break;
                        case 2:
                            await VeiwAppointmnetHandlerAsyns();
                            break;
                        case 3:
                            List<DoctorDetailsDto>? doctors = await _patientService.AllDoctorsAsync();
                            PatientMenu.DisplayDoctorsDeatils(doctors!);
                            break;
                        case 4:
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

        public async Task VeiwAppointmnetHandlerAsyns()
        {
            Console.WriteLine(AdminConstants.BY_DATE);
            Console.WriteLine(AdminConstants.BY_DOCTOR);

            Console.WriteLine(UserConstants.ENTR_OPERATION);
            bool isValidInt = int.TryParse(Console.ReadLine(), out int operationNum);

            if (isValidInt)
            {
                switch (operationNum)
                {
                    case 1:
                        DateTime date = PatientMenu.TakeAppointmentDate();
                        List<AppointementWithStatus>? appointments = await _adminService.AppointmentBydateAsync(date);
                        PatientMenu.DisplayAppoinments(appointments!);
                        break;
                    case 2:
                        int docId = PatientMenu.TakeId();
                        List<AppointementWithStatus>? appointmentDtos = await _adminService.AppointmentByDocIdAsync(docId);
                        PatientMenu.DisplayAppoinments(appointmentDtos!);
                        break;
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
}
