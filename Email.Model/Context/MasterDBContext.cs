using Email.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Model.Context
{
    public class MasterDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5EPP4QL;Database=SSTTEKDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<EMAIL_LOG> EmailLogs { get; set; }
        public DbSet<EMAIL_INFORMATION> EmailInformations { get; set; }
        public DbSet<SMTP_SETTING> SMTPSettings { get; set; }
    }
}
