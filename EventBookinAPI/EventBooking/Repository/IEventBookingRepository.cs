using EventBooking.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Service
{
    public interface IEventBookingRepository
    {
        Task<string> LogInDB(Login ObjLogin);

        Task<string> ForgetPasswordDB(forgetPassword ObjforgetPassword);

        Task<string> RegisterDB(Authentication ObjAuthentication, string userID);

        Task<List<AddEvent>> GetEventsDB();

        Task<List<DBBookingstatusResponse>> GetBookingStatusDB(Bookingstatus ObjBookingstatus);

        Task<string> BookingDB(Booking objBookingRequest, string BookingID, string TicketCategory, string NoofTickets);

        Task<string> CancelBookingDB(BookingCancel ObjBookingCancel);

        Task<string> SubmitforPartnerShipDB(Submitforpartnership ObjSubmitforpartnership, string RequestID);

        Task<List<PendingApproval>> GetPendingApprovalDB();

        Task<string> ParnershipApprovalDB(PartnershipApproval objPartnershipApproval);
    }
}
