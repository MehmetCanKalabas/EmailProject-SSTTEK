using Email.Model.DTOs;
using Email.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service.Infrastructure
{
    public interface IEmailService
    {
        bool AddEmailInformation(AddEmailDTO model);
        Task SaveSentEmailAsync(EMAIL_LOG emailRequest);
        Task<bool> SendMailAsync(EMAIL_LOG emailRequest);
        Task<bool> DeleteMailAsync(int id);
        List<GetEmailDTO> GetEmailInformationList();
        public bool IsTrue();
    }
}
