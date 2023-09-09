using Email.Model.DTOs;
using Email.Service.Infrastructure;
using Email.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Email.Model.Entities;

namespace Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_emailService.IsTrue());
        }

        [HttpGet("GetEmailInformationList")]
        public IActionResult GetEmailInformationList()
        {
            return Ok(_emailService.GetEmailInformationList());
        }

        [HttpPost("AddEmailInformation")]
        public IActionResult AddEmailInformation(AddEmailDTO data)
        {
            return Ok(_emailService.AddEmailInformation(data));
        }

        [HttpDelete("DeleteEmail/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var success = await _emailService.DeleteMailAsync(id);
            if (success)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EMAIL_LOG emailRequest)
        {
            try
            {
                SMTP_SETTING smtpSettings = new SMTP_SETTING();

                var smtpClient = new SmtpClient
                {
                    Host = smtpSettings.HostName,  // SMTP sunucu adresi
                    Port = smtpSettings.Port, // SMTP sunucu portu (genellikle 587 veya 465)
                    EnableSsl = smtpSettings.SSL, // SSL kullanılsın mı?
                    Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password) // SMTP kimlik bilgileri
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings.UserName), // Gönderen e-posta adresi
                    Subject = emailRequest.Subject,
                    Body = emailRequest.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(emailRequest.To);

                if (!string.IsNullOrEmpty(emailRequest.CCC))
                {
                    mailMessage.CC.Add(emailRequest.BCC);
                }

                // BCC (Blind Carbon Copy) eklemek için
                if (!string.IsNullOrEmpty(emailRequest.BCC))
                {
                    mailMessage.Bcc.Add(emailRequest.BCC);
                }

                await smtpClient.SendMailAsync(mailMessage);

                return Ok("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"E-posta gönderme işlemi başarısız oldu: {ex.Message}");
            }
        }
    }
}
