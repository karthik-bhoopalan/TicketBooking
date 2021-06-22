using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class Bookingstatus
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }

    }

    public class BookingstatusResponse
    {
        public List<statusResponse> response { get; set; }
        public BookingstatusResponse()
        {
            response = new List<statusResponse>();
        }

        [NotMapped]
        public string ResponseMessage { get; set; }

    }

    public class statusResponse
    {
        public string UserID { get; set; }
        public string BookingID { get; set; }
        public string Status { get; set; }
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventTimings { get; set; }
        public string EventStatus { get; set; }
        public string EventDetails { get; set; }
    }

    public class DBBookingstatusResponse
    {
        public string UserID { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BookingID { get; set; }
        public string Status { get; set; }
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventTimings { get; set; }
        public string EventStatus { get; set; }
        public string EventDetails { get; set; }
    }
}
