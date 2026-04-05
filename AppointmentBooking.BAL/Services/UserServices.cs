using AppointmentBooking.BAL.Interfaces;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using AppointmentBooking.DAL.Repo;
using AppointmentBooking.Shared;
using AppointmentBooking.Shared.Constatnts;

namespace AppointmentBooking.BAL.Services
{
    public class UserServices : IUserService
    {

        private readonly IUserRepo _userRepo;
        public UserServices()
        {
            _userRepo = new UserRepo();
        }


        public async Task<(string message, bool success, bool isAdmin)> LoginUserAsync(string email, string password) //returing tuple (for one time other wise should use
                                                                                                                      //class that holds like ---->> DATA,STATUS_CODE,MESSAGE,SUCCESS,)

        {
            try
            {
                //getting user
                User? user = await _userRepo.GetUserByEmailAsync(email);

                // if user not exists
                if (user == null) return (UserConstants.USER_NOT_FOUND, false, false);

                //password matching
                if (user.Password == password)
                {
                    AppConfig.UserId = user.Id; // user id
                    if (user.IsAdmin) // user as admin
                    {
                        AppConfig.DoctorId = user.Doctor.Id; //doctor id
                        return (UserConstants.ADMIN, true, true);
                    }
                    else // user as patient
                    {
                        AppConfig.PatientId = user.Patient.Id; //patient id
                        return (UserConstants.PATIENT, true, false);
                    }
                }
                return (UserConstants.INCORRECT_PASSWORD, false, false); // invalid password
            }
            catch
            {
                return (UserConstants.CATCH_ERROR, false, false);
            }
        }
    }
}
