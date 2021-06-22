using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class forgetPassword
    {
        public string LogID { get; set; }
        public string LogInDetail { get; set; }
        public string Password { get; set; }
    }

    public class ForgetPasswordResponse
    {
        public string LogID { get; set; }
        public string ResponseMessage { get; set; }
    }
}
