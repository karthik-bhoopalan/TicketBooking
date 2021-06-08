using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Modal
{
    public class PendingApproval
    {
        public string UserID { get; set; }
        public string AadharNum { get; set; }
        public Byte[] AadharImage { get; set; }
        public Byte[] AdditionalDocImage { get; set; }
        public string AddtionDocDetails { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequestID { get; set; }
        public string Status { get; set; }

    }

    public class PendingApprovalResponse
    {
        public string ResponseMessage { get; set; }
        public List<ApprovalResponse> response { get; set; }

        public PendingApprovalResponse()
        {
            response = new List<ApprovalResponse>();
        }
    }

    public class ApprovalResponse
    {
        public string UserID { get; set; }
        public string AadharNum { get; set; }
        public string AadharImage { get; set; }
        public string AdditionalDocImage { get; set; }
        public string AddtionDocDetails { get; set; }
        public string RequestID { get; set; }
        public string Status { get; set; }
    }
}
