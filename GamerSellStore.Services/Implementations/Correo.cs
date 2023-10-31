using GamerSellStore.Dtos.Request.Correo;
using GamerSellStore.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GamerSellStore.Services.Interfaces;

namespace GamerSellStore.Services.Implementations
{
    public class Correo : ICorreo
    {
        private readonly IOptions<AppSettings> options;
        public Correo(IOptions<AppSettings> _options)
        {
            options = _options;
        }

        public async Task EnviarCorreoAsync(CorreoDtoRequest request)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                using (var client = new SmtpClient(options.Value.emailSenderOptions.Host))
                {
                    client.Port = options.Value.emailSenderOptions.Port;
                    client.EnableSsl = options.Value.emailSenderOptions.EnableSsl;
                    client.Credentials = new NetworkCredential(options.Value.emailSenderOptions.Email, 
                        options.Value.emailSenderOptions.Password);

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(request.Destinatario));
                        emailMessage.Subject = request.Asunto;
                        emailMessage.Body = request.Mensaje;
                        emailMessage.From = new MailAddress(options.Value.emailSenderOptions.Email);

                        await client.SendMailAsync(emailMessage);
                    }
                }
                //SmtpClient smtpClient = new SmtpClient(options.Value.emailSenderOptions.Host)
                //{
                //    Port = 587, // Puerto SMTP de GMX.es
                //    Credentials = new NetworkCredential(options.Value.emailSenderOptions.Email,
                //    options.Value.emailSenderOptions.Password),
                //    EnableSsl = options.Value.emailSenderOptions.EnableSsl, // Habilita SSL
                //};

                //// Crea un mensaje de correo.
                //MailMessage mailMessage = new MailMessage
                //{
                //    From = new MailAddress(options.Value.emailSenderOptions.Email),
                //    Subject = request.Asunto,
                //    Body = request.Mensaje,
                //};

                //// Agrega el destinatario.
                //mailMessage.To.Add(request.Destinatario);


                //using (var client = new SmtpClient(options.Value.emailSenderOptions.Host, options.Value.emailSenderOptions.Port))
                //{
                //    client.EnableSsl = options.Value.emailSenderOptions.EnableSsl;
                //    client.Credentials = new NetworkCredential(options.Value.emailSenderOptions.Email, options.Value.emailSenderOptions.Password);

                //    using (var emailMessage = new MailMessage())
                //    {
                //        emailMessage.To.Add(new MailAddress(request.Destinatario));
                //        emailMessage.Subject = request.Asunto;
                //        emailMessage.Body = request.Mensaje;
                //        emailMessage.From = new MailAddress(options.Value.emailSenderOptions.Email);

                //        await client.SendMailAsync(emailMessage);
                //    }
                //}
            }
            catch
            {
                throw;
            }
        }
    }
}
