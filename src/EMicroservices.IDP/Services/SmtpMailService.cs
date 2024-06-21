﻿using EMicroservices.IDP.Common;
using EMicroservices.IDP.Services.EmailService;
using System.Net;
using System.Net.Mail;

namespace EMicroservices.IDP.Services
{
    public class SmtpMailService : IEmailSender
    {
        private readonly SMTPEmailSetting _settings;
        public SmtpMailService(SMTPEmailSetting settings)
        {
            _settings = settings;
        }

        public void SendEmail(string recipient, string subject, string body, bool isBodyHtml = false, string sender = null)
        {
            var message = new MailMessage(_settings.From, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
                From = new MailAddress(_settings.From, !string.IsNullOrEmpty(sender) ? sender : _settings.From)
            };
            using var client = new SmtpClient(_settings.SMTPServer, _settings.Port)
            {
                EnableSsl = _settings.UseSsl
            };
            if(!string.IsNullOrEmpty(_settings.Username)|| !string.IsNullOrEmpty(_settings.Password))
            {
                client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
            }
            else
            {
                client.UseDefaultCredentials = true;
            }
            client.Send(message);
        }
    }
}
