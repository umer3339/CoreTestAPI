using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using System.IO;
using MailKit.Security;

namespace MailHelper
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendMailNotification(List<string> toEmailIds, string emailTemplate, string batchId)
        {
            var isMailSend = false;
            try
            {

                SmtpClient client = new SmtpClient();
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(_configuration.GetSection("EmailNotification:FromName").Value,
                _configuration.GetSection("EmailNotification:FromEmail").Value);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User",
                "BhaweshDeepak@yahoo.com");
                List<MailboxAddress> toMailAddress = new List<MailboxAddress>();
                toEmailIds.ForEach(item =>
                {
                    var userName = item.Split("@")[0];
                    toMailAddress.Add(new MailboxAddress(userName, item.Trim()));
                });


                message.To.AddRange(toMailAddress);

                message.Subject = batchId;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailTemplate;


                message.Body = bodyBuilder.ToMessageBody();

                try
                {

                    client.Connect(_configuration.GetSection("EmailNotification:SMTPServer").Value,
                        Convert.ToInt32(_configuration.GetSection("EmailNotification:SMTPPort").Value), SecureSocketOptions.StartTls);
                    
                    client.Authenticate("itbasserahbasserah@gmail.com", "vi@pra91");

                    client.Send(message);
                    isMailSend = true;

                }
                catch (Exception ex)
                {
                    await WriteEmailLoger(ex);
                    isMailSend = false;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
            catch (Exception ex) {
                await WriteEmailLoger(ex);
                isMailSend = false;
            }

            return await Task.Run(() => isMailSend);
        }

        private async Task WriteEmailLoger(Exception ex) 
        {

            string filePath = _configuration.GetSection("EmailNotification:LoggerPath").Value;

            using StreamWriter outputFile = new StreamWriter(filePath);
            await Task.Run(() => outputFile.WriteLine($"Message: {ex.Message} " +
                $"  inner exception {ex.InnerException}"));
        }
    }
}
