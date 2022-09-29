using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ReqModels
{
    public class LoginReq
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
