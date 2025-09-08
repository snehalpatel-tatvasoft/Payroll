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

        public async Task<bool> SendMail(MailMessage mailMessage)
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
                mailMessage.To.Clear();
                mailMessage.To.Add(new MailAddress("snehal.patel@tatvasoft.com"));

                client.SendCompleted += (s, e) =>
                {
                    client.Dispose();
                    mailMessage.Dispose();
                };
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(ResponseMessages.EmailMailboxUnavailable))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
