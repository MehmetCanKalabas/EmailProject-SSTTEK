using Email.Model.DTOs;
using Email.Service.Infrastructure;
using Email.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Email.Model.Entities;
using System;
using System.Threading.Tasks;
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

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok (_emailService.GetByIdAsync(id));
        }

        [HttpGet("IsEmailExist/{email}")]
        public IActionResult IsEmailExist(string email)
        {
            return Ok(_emailService.IsEmailExist(email));
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
            var result = await _emailService.SendMailAsync(emailRequest);
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}