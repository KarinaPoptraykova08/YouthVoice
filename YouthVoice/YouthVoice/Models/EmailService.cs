using System.Net.Mail;
using System.Net;

namespace YouthVoice.Models
{
    public class EmailService
    {
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        private readonly string fromEmail;
        string fromPassword = "twft eljx nnmy bxcw";

        public EmailService()
        {
            fromEmail = "youthvoice.burgas@gmail.com";
            FromEmail = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
        }

        public void SendEmail(string toEmail, string subject, string body)
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

        public void SendEmail(string fromEmail, string toEmail, string subject, string body)
        {
            SendEmail(toEmail, subject, body + $"<br><br>This email has been sent by: {fromEmail}");
        }
    }
}
