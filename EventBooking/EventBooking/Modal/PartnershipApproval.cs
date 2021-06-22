using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class PartnershipApproval
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RequestID { get; set; }
    }

    public class PartnershipApprovalResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RequestID { get; set; }
        public string ResponseMessage { get; set; }
    }
}
