using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SendMail(MailMessage mailMessage)
        {
            try
            {
                string SMTPMailServer = _configuration[key: "SmtpCredentials:SMTPMailServer"]!;
                int SMTPMailServerPort = Convert.ToInt32(_configuration[key: "SmtpCredentials:SMTPPort"]);
                string SMTPMailUser = _configuration[key: "SmtpCredentials:SMTPMailUser"]!;
                string SMTPMailPassword = _configuration[key: "SmtpCredentials:SMTPMailPassword"]!;
                bool EnableSsl = _configuration[key: "SmtpCredentials:SMTPEnableSsl"]?.ToLower() == "true" ? true : false;
                string fromEmail = _configuration[key: "SmtpCredentials:SMTPFrom"]!;

                SmtpClient client = new SmtpClient(SMTPMailServer, SMTPMailServerPort)
                {
                    Credentials = new System.Net.NetworkCredential(SMTPMailUser, SMTPMailPassword),
                    EnableSsl = EnableSsl
                };

                mailMessage.From = new MailAddress(fromEmail);

                client.SendCompleted += (s, e) =>
                {
                    client.Dispose();
                    mailMessage.Dispose();
                };
                client.Send(mailMessage);
                return ResponseMessages.EmailSentSuccessfully;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(ResponseMessages.EmailMailboxUnavailable))
                {
                    return ResponseMessages.EmailMailboxUnavailable;
                }
                else
                {
                    return ResponseMessages.EmailSentFailure;
                }
            }
        }
    }
}
