using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iPatient.Helpers
{
    public static class DataValidation
    {
        public static bool ValidateEmail (string email)
        {
            if (email == null)
                return false;

            Regex correctEmail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@"
                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            Match emailMatch = correctEmail.Match(email);

            return emailMatch.Success;
        }
    }
}
