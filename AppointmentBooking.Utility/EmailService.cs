using AppointmentBooking.Shared;
using System.Net;
using System.Net.Mail;

namespace AppointmentBooking.Utility
{
    public static class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string content)
        {
            SmtpClient smtp = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(AppConfig.Username, AppConfig.Password),
                EnableSsl = true,
                UseDefaultCredentials = false,
            };

            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress("jiwan.step2gen@gmail.com");

            MailMessage message = new(from, to)
            {
                Body = content,
                Subject = subject,

            };


            await smtp.SendMailAsync(message);
        }
    }
}
