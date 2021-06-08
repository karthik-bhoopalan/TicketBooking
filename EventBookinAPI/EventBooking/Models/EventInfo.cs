using System;
using System.Collections.Generic;

#nullable disable

namespace EventBooking.Models
{
    public partial class EventInfo
    {
        public int RecId { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventTimings { get; set; }
        public string EventStatus { get; set; }
    }
}
