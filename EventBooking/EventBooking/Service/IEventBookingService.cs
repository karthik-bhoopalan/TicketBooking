using EventBooking.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Service
{
    public interface IEventBookingService
    {
        Task<LoginResponse> LogIn(Login ObjLogin);

        Task<ForgetPasswordResponse> ForgetPassword(forgetPassword ObjforgetPassword);

        Task<AuthenticationResponse> Register(Authentication ObjAuthentication, string userID);

        Task<AddEventResponse> GetEvents();

        Task<BookingstatusResponse> GetBookingStatus(Bookingstatus ObjBookingstatus);

        Task<BookingResponse> Booking(Booking objBookingRequest, string BookingID);

        Task<BookingCancelResponse> CancelBooking(BookingCancel ObjBookingCancel);

        Task<SubmitforpartnershipResponse> SubmitforPartnerShip(Submitforpartnership ObjSubmitforpartnership, string RequestID);

        Task<PendingApprovalResponse> GetPendingApproval();

        Task<PartnershipApprovalResponse> ParnershipApproval(PartnershipApproval objPartnershipApproval);

        bool SendOTP(string from, string to, string subject, string body);

        Task<string> OTPGeneration(ObjOTPGenerationRequest ObjOTPGeneration, string otp);

        Task<string> OTPVerification(OTPVerificationRequest ObjOTPVerificationRequest);
    }
}
