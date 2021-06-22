using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class BookingCancel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string BookingID { get; set; }
    }

    public class BookingCancelResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string BookingID { get; set; }
        public string ResponseMessage { get; set; }
    }
}
