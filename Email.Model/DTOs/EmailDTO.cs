using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Model.DTOs
{
    public class AddEmailDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class GetEmailDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
