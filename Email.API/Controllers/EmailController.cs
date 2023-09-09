using Email.Model.DTOs;
using Email.Service.Infrastructure;
using Email.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Email.Model.Entities;
using System;
using System.Threading.Tasks; // Eksik using bildirimi
using MimeKit;
using MailKit.Net.Smtp;

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
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Can", "mehmetcankalabas.sttek@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", "kalabascancan@gmail.com");
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = emailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = emailRequest.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("mehmetcankalabas.sttek@gmail.com", "zttmnjebzobjfndv");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return Ok();
        }
    }
}