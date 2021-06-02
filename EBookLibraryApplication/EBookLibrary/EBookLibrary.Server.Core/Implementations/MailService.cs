using EBookLibrary.DTOs.Commons;
using EBookLibrary.Models.Settings;
using EBookLibrary.Server.Core.Abstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class MailService : IMailService
    {

        private readonly MailConfig _mailConfig;
        public MailService(IOptions<MailConfig> mailconfig)
        {
            _mailConfig = mailconfig.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Views\\Email\\Mail2.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", mailRequest.Name)
                                .Replace("[Message]", mailRequest.RecipientMail)
                                .Replace("[link]", mailRequest.Link);


            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailConfig.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.RecipientMail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailConfig.Mail, _mailConfig.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);   
            
        }  
    }
}
