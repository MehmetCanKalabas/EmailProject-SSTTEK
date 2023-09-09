using Email.Model.DTOs;
using Email.Service.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("AddEmailInformation")]
        public IActionResult AddEmailInformation(AddEmailDTO data)
        {
            return Ok(_emailService.AddEmailInformation(data));
        }
        [HttpGet("GetEmailInformationList")]
        public IActionResult GetEmailInformationList()
        {
            return Ok(_emailService.GetEmailInformationList());
        }
    }
}
