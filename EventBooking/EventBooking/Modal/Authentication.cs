using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class Authentication
    {
        public string UserName { get; set; }
        public string password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public string CategoryID { get; set; }
    }

    public class AuthenticationResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public string ResponseMessage { get; set; }
        public string OTP { get; set; }
    }
}
