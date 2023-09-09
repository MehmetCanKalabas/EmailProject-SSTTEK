using Email.Model.Context;
using Email.Model.DTOs;
using Email.Model.Entities;
using Email.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service.Services
{
    public class EmailService : IEmailService
    {
        public bool IsTrue()
        {
            return true;
        }
        #region Email_info
        public bool AddEmailInformation(AddEmailDTO model)
        {
            bool result = false;
            if (!model.Email.IsNullOrEmpty())
            {
                using (MasterDBContext db = new MasterDBContext())
                {
                    EMAIL_INFORMATION email = db.EmailInformations.Where(w => w.Email.Equals(model.Email)).FirstOrDefault();
                    if (email == null)
                    {
                        email = new EMAIL_INFORMATION();
                        email.Email = model.Email;
                        email.Name = model.Name;
                        email.Surname = model.Surname;
                        db.Add(email);
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            return result;
        }
        public List<GetEmailDTO> GetEmailInformationList()
        {
            List<GetEmailDTO> result = null;

            using (MasterDBContext db = new MasterDBContext())
            {
                var list = db.EmailInformations.Where(w => !w.IsDelete).ToList();
                if (list != null && list.Count > 0)
                {
                    result = new List<GetEmailDTO>();
                    foreach (var email in list)
                    {
                        result.Add(new GetEmailDTO()
                        {
                            Id = email.Id,
                            Email = email.Email,
                            Name = email.Name,
                            Surname = email.Surname
                        });
                    }
                }
            }
            return result;
        }
        #endregion
        #region Email_Log

        #endregion
    }
}
