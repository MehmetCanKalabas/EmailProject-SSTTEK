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
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (System.Text.RegularExpressions.Regex.IsMatch(email, pattern))
            {
                return true;
            }
            return false;
        }
    }
}
