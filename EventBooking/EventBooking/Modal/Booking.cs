using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class Booking
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EventID { get; set; }
        public string SubEventID { get; set; }
        public string TotalAmount { get; set; }
        public List<BookingRequest> Request { get; set; }
        public Booking()
        {
            Request = new List<BookingRequest>();
        }
    }

    public class BookingRequest
    {
        public string TicketCategory { get; set; }
        public string NoofTickets { get; set; }
    }


    public class BookingResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string BookingID { get; set; }
        public string ResponseMessage { get; set; }
    }
}
