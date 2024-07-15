using Demo.DAL.Models;
using Microsoft.CodeAnalysis.Emit;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("3alam.t@gmail.com", "emgpiqeofdwgitke");
            client.Send("3alam.t@gmail.com", email.Recipient, email.Subject, email.Body);
        }
    }
}
