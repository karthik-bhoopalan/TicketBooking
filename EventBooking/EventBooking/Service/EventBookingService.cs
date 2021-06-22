using EventBooking.Data;
using EventBooking.Modal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBooking.Service
{
    public class EventBookingService : IEventBookingService
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ILoggerService _logger;

        private readonly IEventBookingRepository _eventBookingRepository;

        public EventBookingService(IEventBookingRepository eventBookingRepository, ILoggerService logger)
        {
            _eventBookingRepository = eventBookingRepository ?? throw new ArgumentNullException(nameof(eventBookingRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<LoginResponse> LogIn(Login ObjLogin)
        {
            LoginResponse ObjLoginResponse = new LoginResponse();
                if (string.IsNullOrEmpty(ObjLogin.LogInDetail))
                {
                    ObjLoginResponse.ResponseMessage = "Please Select whether the Login ID is UserName/Email ID/ Phone Num";
                    ObjLoginResponse.LogID = ObjLogin.LogID;
                    return ObjLoginResponse;
                }

                if (string.IsNullOrEmpty(ObjLogin.LogID))
                {
                    ObjLoginResponse.ResponseMessage = "Please enter UserName/Email ID/ Phone Num";
                    ObjLoginResponse.LogID = ObjLogin.LogID;
                    return ObjLoginResponse;
                }

                if (string.IsNullOrEmpty(ObjLogin.Password))
                {
                    ObjLoginResponse.ResponseMessage = "Please enter Password";
                    ObjLoginResponse.LogID = ObjLogin.LogID;
                    return ObjLoginResponse;
                }

                if (ObjLogin.LogInDetail.ToUpper() != "EMAIL" && ObjLogin.LogInDetail.ToUpper() != "USERNAME" && ObjLogin.LogInDetail.ToUpper() != "PHONENUM")
                {
                    ObjLoginResponse.ResponseMessage = "Login Method is invalid, Please Try again!!!";
                    ObjLoginResponse.LogID = ObjLogin.LogID;
                    return ObjLoginResponse;
                }

            //string password = Decrypt(ObjLogin.Password, "1234567891");
            //ObjLogin.Password = password;

                string ResponseMessage = await _eventBookingRepository.LogInDB(ObjLogin);

                ObjLoginResponse.ResponseMessage = ResponseMessage;
                ObjLoginResponse.LogID = ObjLogin.LogID;
            return ObjLoginResponse;
        }

        public async Task<ForgetPasswordResponse> ForgetPassword(forgetPassword ObjforgetPassword)
        {
            ForgetPasswordResponse objForgetPasswordResponse = new ForgetPasswordResponse();
                if (string.IsNullOrEmpty(ObjforgetPassword.LogInDetail))
                {
                    objForgetPasswordResponse.ResponseMessage = "Please Select whether the Login ID is UserName/Email ID/ Phone Num";
                    objForgetPasswordResponse.LogID = ObjforgetPassword.LogID;
                    return objForgetPasswordResponse;
                }

                if (string.IsNullOrEmpty(ObjforgetPassword.LogID))
                {
                    objForgetPasswordResponse.ResponseMessage = "Please enter UserName/Email ID/ Phone Num";
                    objForgetPasswordResponse.LogID = ObjforgetPassword.LogID;
                    return objForgetPasswordResponse;
                }

                if (string.IsNullOrEmpty(ObjforgetPassword.Password))
                {
                    objForgetPasswordResponse.ResponseMessage = "Please enter Password";
                    objForgetPasswordResponse.LogID = ObjforgetPassword.LogID;
                    return objForgetPasswordResponse;
                }

                if (ObjforgetPassword.LogInDetail.ToUpper() != "EMAIL" && ObjforgetPassword.LogInDetail.ToUpper() != "USERNAME" && ObjforgetPassword.LogInDetail.ToUpper() != "PHONENUM")
                {
                    objForgetPasswordResponse.ResponseMessage = "Login Method is invalid, Please Try again!!!";
                    objForgetPasswordResponse.LogID = ObjforgetPassword.LogID;
                    return objForgetPasswordResponse;
                }

                string ResponseMessage = await _eventBookingRepository.ForgetPasswordDB(ObjforgetPassword);

                objForgetPasswordResponse.ResponseMessage = ResponseMessage;
                objForgetPasswordResponse.LogID = ObjforgetPassword.LogID;
            return objForgetPasswordResponse;
        }

        public async Task<AuthenticationResponse> Register(Authentication ObjAuthentication,string userID)
        {
            AuthenticationResponse ObjAuthenticationResponse = new AuthenticationResponse();

            Random rnd = new Random();
            int otp = rnd.Next(1000, 9999);

            string ResponseMessage = await _eventBookingRepository.RegisterDB(ObjAuthentication, userID, otp);

            if(ResponseMessage == "Customer Created Successufully")
            {
                string msg = "your otp from Event Booking Registration is " + otp;

                bool otpCheck = SendOTP("karthik.moni1994@gmail.com", ObjAuthentication.EmailID, "Event Booking Authentication OTP", msg);
            }

            ObjAuthenticationResponse.UserID = userID;
            ObjAuthenticationResponse.UserName = ObjAuthentication.UserName;
            ObjAuthenticationResponse.EmailID = ObjAuthentication.EmailID;
            ObjAuthenticationResponse.PhoneNum = ObjAuthentication.PhoneNum;
            ObjAuthenticationResponse.ResponseMessage = ResponseMessage;
            ObjAuthenticationResponse.OTP = Convert.ToString(otp);
            return ObjAuthenticationResponse;
        }


        private string Decrypt(string textToDecrypt, string key)
        {
             RijndaelManaged rijndaelCipher = new RijndaelManaged();
             rijndaelCipher.Mode = CipherMode.CBC;
             rijndaelCipher.Padding = PaddingMode.PKCS7;
             byte[] IV = new byte[0x10];
             rijndaelCipher.KeySize = 0X100;
             rijndaelCipher.BlockSize = 0X80;
             byte[] keyBytes = new byte[0x20];
             byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
             byte[] pwdBytes = Encoding.UTF8.GetBytes(key);


             int len = pwdBytes.Length;
             if (len > keyBytes.Length)
             {
                 len = keyBytes.Length;
             }
             Array.Copy(pwdBytes, keyBytes, len);
             rijndaelCipher.Key = keyBytes;
             rijndaelCipher.IV = IV;
             byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
             return Encoding.UTF8.GetString(plainText);
        }

        public async Task<AddEventResponse> GetEvents()
        {
            AddEventResponse ObjAddEventResponse = new AddEventResponse();
                var events = await _eventBookingRepository.GetEventsDB();

                if (events.Count > 0)
                {
                    for (int i = 0; i < events.Count; i++)
                    {
                        Response ObjResponse = new Response();
                        ObjResponse.EventID = events[i].EventID;
                        ObjResponse.EventName = events[i].EventName;
                        ObjResponse.EventStatus = events[i].EventStatus;
                        ObjResponse.EventTimings = events[i].EventTimings;
                        ObjAddEventResponse.response.Add(ObjResponse);
                    }
                    ObjAddEventResponse.ResponseMessage = "Success";
                }
                else
                {
                    ObjAddEventResponse.ResponseMessage = "No events available";
                }
            return ObjAddEventResponse;
        }

        public async Task<BookingstatusResponse> GetBookingStatus(Bookingstatus ObjBookingstatus)
        {
            BookingstatusResponse ObjBookingstatusResponse = new BookingstatusResponse();
                var events = await _eventBookingRepository.GetBookingStatusDB(ObjBookingstatus);
                if (events.Count > 0)
                {
                    for (int i = 0; i < events.Count; i++)
                    {
                        statusResponse ObjResponse = new statusResponse();
                        ObjResponse.UserID = events[i].UserID;
                        ObjResponse.BookingID = events[i].BookingID;
                        ObjResponse.Status = events[i].Status;
                        ObjResponse.EventID = events[i].EventID;
                        ObjResponse.EventName = events[i].EventName;
                        ObjResponse.EventTimings = events[i].EventTimings;
                        ObjResponse.EventStatus = events[i].EventStatus;
                        ObjResponse.EventDetails = events[i].EventDetails;
                        ObjBookingstatusResponse.response.Add(ObjResponse);
                    }
                    ObjBookingstatusResponse.ResponseMessage = "Booking Status found successfully";
                }
                else
                {
                    ObjBookingstatusResponse.ResponseMessage = "No Bookings found";
                }
            return ObjBookingstatusResponse;
        }


        public async Task<BookingResponse> Booking(Booking objBookingRequest, string BookingID)
        {
            BookingResponse objBookingResponse = new BookingResponse();
            string TicketCategory = "", NoofTickets = "";
            
            if (string.IsNullOrEmpty(objBookingRequest.EventID))
            {
                objBookingResponse.ResponseMessage = "Event not selected, please try again!!!";
                objBookingResponse.UserID = objBookingRequest.UserID;
                objBookingResponse.UserName = objBookingRequest.UserName;
                return objBookingResponse;
            }
            
            if (string.IsNullOrEmpty(objBookingRequest.SubEventID))
            {
                objBookingResponse.ResponseMessage = "Match not selected, please try again!!!";
                objBookingResponse.UserID = objBookingRequest.UserID;
                objBookingResponse.UserName = objBookingRequest.UserName;
                return objBookingResponse;
            }
            
            if (string.IsNullOrEmpty(objBookingRequest.TotalAmount))
            {
                objBookingResponse.ResponseMessage = "TotalAmount not generated, please try again!!!";
                objBookingResponse.UserID = objBookingRequest.UserID;
                objBookingResponse.UserName = objBookingRequest.UserName;
                return objBookingResponse;
            }
            
            if (objBookingRequest.Request.Count > 0)
            {
                if (objBookingRequest.Request.Count == 1)
                {
                    TicketCategory = Convert.ToString(objBookingRequest.Request[0].TicketCategory);
                    NoofTickets = Convert.ToString(objBookingRequest.Request[0].NoofTickets);
                }
                else
                {
                    for (int i = 0; i < objBookingRequest.Request.Count; i++)
                    {
                        TicketCategory = TicketCategory + Convert.ToString(objBookingRequest.Request[i].TicketCategory) + "~";
                        NoofTickets = NoofTickets + Convert.ToString(objBookingRequest.Request[i].NoofTickets) + "~";
                    }
                    TicketCategory = TicketCategory.Substring(0, TicketCategory.Length - 1);
                    NoofTickets = NoofTickets.Substring(0, NoofTickets.Length - 1);
                }
            
                string ResponseMessage = await _eventBookingRepository.BookingDB(objBookingRequest, BookingID, TicketCategory, NoofTickets);
            
                objBookingResponse.ResponseMessage = ResponseMessage;
                objBookingResponse.BookingID = BookingID;
                objBookingResponse.UserID = objBookingRequest.UserID;
                objBookingResponse.UserName = objBookingRequest.UserName;
            
            }
            else
            {
                objBookingResponse.ResponseMessage = "Ticket Category not selected";
                objBookingResponse.UserID = objBookingRequest.UserID;
                objBookingResponse.UserName = objBookingRequest.UserName;
            }
            return objBookingResponse;
        }

        public bool SendOTP(string from, string to, string subject, string body)
        {
            bool f = false;
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                //SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
                //smtpClient.Send(mailMessage);

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                smt.EnableSsl = true;
                smt.UseDefaultCredentials = false;
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = from; //Your Email ID  
                ntwd.Password = "1936chaitra"; // Your Password
                smt.Credentials = ntwd;
                smt.Port = 587;

                smt.Send(mailMessage);

                f = true;
            }
            catch (Exception ex)
            {
                f = false;
            }
            return f;
        }

        public async Task<string> OTPGeneration(ObjOTPGenerationRequest ObjOTPGeneration, string otp)
        {
            string ResponseMessage = await _eventBookingRepository.OTPGenerationDB(ObjOTPGeneration, otp);
            return ResponseMessage;
        }

        public async Task<string> OTPVerification(OTPVerificationRequest ObjOTPVerificationRequest)
        {
            string ResponseMessage = await _eventBookingRepository.OTPVerificationDB(ObjOTPVerificationRequest);
            return ResponseMessage;
        }

        public async Task<BookingCancelResponse> CancelBooking(BookingCancel ObjBookingCancel)
        {
            BookingCancelResponse objBookingCancelResponse = new BookingCancelResponse();
                string ResponseMessage = await _eventBookingRepository.CancelBookingDB(ObjBookingCancel);

                objBookingCancelResponse.ResponseMessage = ResponseMessage;
                objBookingCancelResponse.UserID = ObjBookingCancel.UserID;
                objBookingCancelResponse.UserName = ObjBookingCancel.UserName;
                objBookingCancelResponse.BookingID = ObjBookingCancel.BookingID;
            return objBookingCancelResponse;
        }

        public async Task<SubmitforpartnershipResponse> SubmitforPartnerShip(Submitforpartnership ObjSubmitforpartnership, string RequestID)
        {
            SubmitforpartnershipResponse objSubmitforpartnershipResponse = new SubmitforpartnershipResponse();
            string ResponseMessage = await _eventBookingRepository.SubmitforPartnerShipDB(ObjSubmitforpartnership, RequestID);

                objSubmitforpartnershipResponse.ResponseMessage = ResponseMessage;
                objSubmitforpartnershipResponse.UserID = ObjSubmitforpartnership.UserID;
                objSubmitforpartnershipResponse.UserName = ObjSubmitforpartnership.UserName;
                objSubmitforpartnershipResponse.PartnerShipRequestID = RequestID;
            return objSubmitforpartnershipResponse;
        }

        public async Task<PendingApprovalResponse> GetPendingApproval()
        {
            PendingApprovalResponse objPendingApproval = new PendingApprovalResponse();
                var events = await _eventBookingRepository.GetPendingApprovalDB();
                if (events.Count > 0)
                {
                    for (int i = 0; i < events.Count; i++)
                    {
                        ApprovalResponse ObjResponse = new ApprovalResponse();
                        ObjResponse.UserID = events[i].UserID;
                        ObjResponse.AadharNum = events[i].AadharNum;
                        ObjResponse.AadharImage = Convert.ToBase64String(events[i].AadharImage);
                        ObjResponse.AdditionalDocImage = Convert.ToBase64String(events[i].AdditionalDocImage);
                        ObjResponse.AddtionDocDetails = events[i].AddtionDocDetails;
                        ObjResponse.RequestID = events[i].RequestID;
                        ObjResponse.Status = events[i].Status;
                        objPendingApproval.response.Add(ObjResponse);
                    }
                    objPendingApproval.ResponseMessage = "Sucess";
                }
                else
                {
                    objPendingApproval.ResponseMessage = "No new Reqests found for Customer Partnership Approval";
                }
            return objPendingApproval;
        }

        public async Task<PartnershipApprovalResponse> ParnershipApproval(PartnershipApproval objPartnershipApproval)
        {
            PartnershipApprovalResponse objPartnershipApprovalResponse = new PartnershipApprovalResponse();
            string ResponseMessage = await _eventBookingRepository.ParnershipApprovalDB(objPartnershipApproval);

                objPartnershipApprovalResponse.ResponseMessage = ResponseMessage;
                objPartnershipApprovalResponse.UserID = objPartnershipApproval.UserID;
                objPartnershipApprovalResponse.UserName = objPartnershipApproval.UserName;
                objPartnershipApprovalResponse.RequestID = objPartnershipApproval.RequestID;
            return objPartnershipApprovalResponse;
        }
    }
}
