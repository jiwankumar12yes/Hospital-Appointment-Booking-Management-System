using System.Text.RegularExpressions;

namespace AppointmentBooking.BAL.Validations
{
    public class UserValidations
    {
        public static bool IsValidEmail(string email)
        {
            Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.IgnoreCase);
            return validateEmailRegex.IsMatch(email);
        }
    }
}
