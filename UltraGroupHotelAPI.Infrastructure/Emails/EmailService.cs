using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using UltraGroupHotelAPI.Application.Contracts.Infrastructure;
using UltraGroupHotelAPI.Application.Models.Email;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Infrastructure.Emails
{
    public class EmailService : IEmailServices
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(Email email, Traveler traveler)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var pathImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/Yes_Check_Circle.svg/800px-Yes_Check_Circle.svg.png";
            var pathImageUltraGroup = "https://static.wixstatic.com/media/d6a2d0_0bb8a5e8dc1b4e348de2e86e898566f9~mv2.png";

            var hmtlContent = "<!DOCTYPE html>" +
                "<html lang='es'>" +
                "<head>  <meta charset='UTF-8'>  " +
                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>  " +
                "<style>    " +
                "body {font-family: Arial, sans-serif; margin: 20px; padding: 30px; background-color: #f4f4f4; color: #333;} " +
                "h1 {margin-bottom: 20px; text-align: center; } " +
                "p {text-align: justify; font-size: 20px; color: #000;}" +
                "img {max-width: 10%; display: block; margin: auto; } " +
                "</style>" +
                "</head>" +
                "<body> " +
                "<h1>Reservation Message Created Successfully</h1>  " +
                $"<img src={pathImage} alt='Satisfied User' style='max-width: 20%;'>  " +
                $"<p>¡Hello {traveler?.FirstName} {traveler?.LastName}!</p>  " +
                $"<p>Congratulations! Your reservation has been created successfully. Thank you for registering on our site. We hope you enjoy our services!</p>  " +
                "<p>If you have any questions or need assistance, do not hesitate to contact us.</p>  " +
                "<p>Best regards." +
                "<br><br>UltraGroup by Juan Herrera.</p>" +
                $"<img src='{pathImageUltraGroup}' alt='Satisfied User' style='max-width: 15%;'>  " +
                "</body>" +
                "</html>";

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var senGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, hmtlContent);
            var respone = await client.SendEmailAsync(senGridMessage);

            if (respone.StatusCode == System.Net.HttpStatusCode.Accepted || respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            _logger.LogError("The email could not be sent, there are errors");
            return false;
        }
    }
}
