using Email.Model.DTOs;
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
        List<GetEmailDTO> GetEmailInformationList();
        public bool IsTrue();
    }
}
