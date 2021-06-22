using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class Submitforpartnership
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string AadharNum { get; set; }
        public string AadharPic { get; set; }
        public string AddtionalDocument { get; set; }
        public string AddtionalDocumentDetail { get; set; }
    }

    public class SubmitforpartnershipResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PartnerShipRequestID { get; set; }
        public string ResponseMessage { get; set; }

    }
}
