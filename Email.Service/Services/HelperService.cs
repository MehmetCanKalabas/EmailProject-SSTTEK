using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service.Services
{
    public class HelperService
    {
        public static bool IsValidMail(string email)
        {
            if (email.Contains("@"))
            {
                return true;
            }
            return false;
        }
    }
}
