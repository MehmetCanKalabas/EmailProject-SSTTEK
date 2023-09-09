using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Model.Entities
{
    public class SMTP_SETTING : BASE_ENTITY
    {
        public string HostName { get; set; }
        public Int32 Port { get; set; }
        public bool SSL { get; set; } = true;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
