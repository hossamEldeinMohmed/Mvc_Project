using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Mvc_Project.Models.Repositorys
{
    public class EmailSenderRepository : IEmailSender
    {
       
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;


        public EmailSenderRepository(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser, "She Shares"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

           /* mailMessage.ReplyToList.Add(new MailAddress("famemad51@yahoo.com"));*/


            mailMessage.To.Add(email);
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error sending email", ex);
            }

            

            return Task.CompletedTask;
        }

    

    }

}
