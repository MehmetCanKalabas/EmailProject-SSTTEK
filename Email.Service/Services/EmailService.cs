using Email.Model.Context;
using Email.Model.DTOs;
using Email.Model.Entities;
using Email.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;

namespace Email.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTP_SETTING _smtpSetting;
        private readonly MasterDBContext _masterDBContext;
        public EmailService(IOptions<SMTP_SETTING> smtpSetting, MasterDBContext masterDBContext)
        {
            _smtpSetting = smtpSetting.Value;
            _masterDBContext = masterDBContext;
        }

        #region Email_info
        public Task GetByIdAsync(int id)
        {
            var result = _masterDBContext.EmailInformations.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public Task<bool> IsEmailExist(string email)
        {
            throw new NotImplementedException();
        }

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

        public async Task<bool> DeleteMailAsync(int id)
        {
            using (MasterDBContext db = new MasterDBContext())
            {
                var mailToDelete = await db.EmailInformations.FirstOrDefaultAsync(x => x.Id == id);
                if (mailToDelete != null)
                {
                    mailToDelete.IsDelete = true;
                    db.SaveChanges();
                }
            }
            return true;
        }
        #endregion
        #region Email_Log

        public async Task<bool> SendMailAsync(EMAIL_LOG emailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("MEHMETCAN", _smtpSetting.UserName);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("Alıcı", emailRequest.To);
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

            await SaveSentEmailAsync(emailRequest);
            return true;
        }

        public async Task SaveSentEmailAsync(EMAIL_LOG emailRequest)
        {
            using (MasterDBContext masterDBContext = new MasterDBContext())
            {
                await _masterDBContext.EmailLogs.AddAsync(emailRequest);
                await _masterDBContext.SaveChangesAsync();
            }
            //var emailLog = new EMAIL_LOG
            //{
            //    Subject = emailRequest.Subject,
            //    Body = emailRequest.Body,
            //    CreatedDate = DateTime.Now,
            //    CreatedBy = emailRequest.CreatedBy,
            //};
        }

    }
}
#endregion