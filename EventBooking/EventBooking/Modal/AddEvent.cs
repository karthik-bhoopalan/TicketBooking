using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class AddEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EventID { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventTimings { get; set; }
        [Required]
        public string EventStatus { get; set; }

    }


    public class AddEventResponse
    {
        public string ResponseMessage { get; set; }
        public  List<Response> response { get; set; }

        public AddEventResponse()
        {
            response = new List<Response>();
        }

    }


    public class Response
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventTimings { get; set; }
        public string EventStatus { get; set; }
    }
}
