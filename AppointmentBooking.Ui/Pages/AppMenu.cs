using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.BAL.Services;
using AppointmentBooking.BAL.Validations;
using AppointmentBooking.Shared.Constatnts;
using AppointmentBooking.Shared.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentBooking.Ui.Pages
{
    public class AppMenu
    {
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;

        public AppMenu()
        {
            _userService = new UserServices();
            _patientService = new PatientService();
        }

        public async Task ShowAppMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(UserConstants.LOGIN);
                Console.WriteLine(UserConstants.REGISTER);
                Console.WriteLine(UserConstants.QUIT);

                Console.WriteLine(UserConstants.ENTR_OPERATION);
                bool isValidInt = int.TryParse(Console.ReadLine(), out int operationNum);

                if (isValidInt)
                {
                    switch (operationNum)
                    {
                        case 1:
                            await LoginHandlerAsync();
                            break;
                        case 2:
                            await AddPatientHandlerAsync();
                            break;
                        case 3:
                            Environment.Exit(0);
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

        public async Task LoginHandlerAsync()
        {
            string email = TakeValidEmail();
            string password = TakeValidPassword();

            (string message, bool success, bool isAdmin) = await _userService.LoginUserAsync(email, password);

            if (success)
            {
                Console.WriteLine(message);
                if (isAdmin)
                {
                    await new AdminMenu().ShowAdminMenu();
                }
                else
                {
                    await new PatientMenu().ShowPatientmenu();
                }
            }
            else
            {
                Console.WriteLine(message);
            }
            //Console.WriteLine(UserConstants.PRESS_ANY_KEY);
            //Console.ReadKey();
        }

        public static string TakeValidEmail()
        {
            bool isValidEmail = false;
            string email = string.Empty;

            while (!isValidEmail)
            {
                Console.WriteLine(UserConstants.ENTR_EMAIL);
                email = Console.ReadLine()!;

                if (email.IsNullOrEmpty()) Console.WriteLine(UserConstants.EMAIL_REQ);
                else
                {
                    isValidEmail = UserValidations.IsValidEmail(email);

                    if (!(isValidEmail)) Console.WriteLine(UserConstants.INVALID_EMAIL_FORMAT);
                }
            }

            return email;
        }

        public static string TakeValidPassword()
        {
            string password = string.Empty;
            bool isValidPassword = false;

            while (!isValidPassword)
            {
                Console.WriteLine(UserConstants.ENTR_PASSWORD);
                password = Console.ReadLine()!;

                if (password.IsNullOrEmpty()) Console.WriteLine(UserConstants.PASSWORD_REQ);
                else isValidPassword = true;
            }
            return password;
        }

        public static string TakevalidName()
        {
            string name = string.Empty;
            bool isValidName = false;

            while (!isValidName)
            {
                Console.WriteLine(UserConstants.ENTR_NAME);
                name = Console.ReadLine()!;

                if (name.IsNullOrEmpty()) Console.WriteLine(UserConstants.Name_REQ);
                else isValidName = true;
            }
            return name;
        }

        public static int TakevalidAge()
        {
            int age = default;
            bool isValidAge = false;

            while (!isValidAge)
            {
                Console.WriteLine(UserConstants.ENTR_Age);
                bool isInt = int.TryParse(Console.ReadLine(), out age);

                if (!isInt) Console.WriteLine(PatientConstants.INVALID_FORMAT);
                if (age <= 0 || age > 120) Console.WriteLine(UserConstants.Age_VALID);
                else isValidAge = true;
            }
            return age;
        }

        public static long TakePhoneNumber()
        {
            long phone = default;
            bool isValidPhone = false;

            while (!isValidPhone)
            {
                Console.WriteLine(UserConstants.ENTR_Phone);
                bool isInt = long.TryParse(Console.ReadLine(), out phone);

                string phoneString = phone.ToString();

                if (phoneString.IsNullOrEmpty()) Console.WriteLine(UserConstants.PHONE_REQ);
                if (phoneString.Length != 10) Console.WriteLine(UserConstants.Phone_VALID);
                else isValidPhone = true;
            }
            return phone;
        }


        //add user then add patient
        public async Task AddPatientHandlerAsync()
        {
            string email = TakeValidEmail();
            string password = TakeValidPassword();
            string name = TakevalidName();
            int age = TakevalidAge();
            long phone = TakePhoneNumber();
            string phoneNumber = phone.ToString();

            NewlyPatientDto newlyPatient = new()
            {
                Email = email,
                Password = password,
                Name = name,
                Age = age,
                PhoneNumber = phoneNumber,
            };

            bool added = await _patientService.AddNewPatientAsync(newlyPatient);

            if (added) Console.WriteLine(UserConstants.PATIENT_ADDED);
            else Console.WriteLine(UserConstants.PATIENT_NOT_ADDED);
        }
    }
}
