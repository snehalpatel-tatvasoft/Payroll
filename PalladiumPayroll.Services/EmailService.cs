using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.Helper;
using System.Net.Mail;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services
{
    public class EmailService
    {
        private readonly SmtpSettings? _smtpSetting;
        public EmailService(IConfiguration configuration)
        {
            _smtpSetting = AppSettingsConfig.GetSection<SmtpSettings>(configuration, sectionName: "SmtpCredentials");
        }

        public string SendMail(MailMessage mailMessage)
        {
            try
            {
                string SMTPMailServer = _smtpSetting.SMTPMailServer;
                int SMTPMailServerPort = _smtpSetting.SMTPPort;
                string SMTPMailUser = _smtpSetting.SMTPMailUser;
                string SMTPMailPassword = _smtpSetting.SMTPMailPassword;
                bool EnableSsl = _smtpSetting.SMTPEnableSsl;
                string fromEmail = _smtpSetting.SMTPFrom;

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
