using System.Net.Mail;
using System.Net;

namespace YouthVoice.Models
{
    public class EmailService
    {
        private readonly string fromEmail = "youthvoice.burgas@gmail.com";
        string fromPassword = "twft eljx nnmy bxcw";

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
