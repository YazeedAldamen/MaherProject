using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try 
            { 
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("/Email", "/Password"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("/Email"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

           
                    await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                // ex.Message contains details about the error
            }
        }
    }
}
