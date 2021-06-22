using EventBooking.Data;
using EventBooking.DataBase;
using EventBooking.Modal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventBooking.Service
{
    public class EventBookingRepository : IEventBookingRepository
    {
        //private readonly IEventBookingService _eventBookingService;
        private readonly IDBLayer _IDBLayercs;

        public EventBookingRepository(IDBLayer IDBLayercs)
        {
            _IDBLayercs = IDBLayercs ?? throw new ArgumentNullException(nameof(IDBLayercs));
        }


        public async Task<string> LogInDB(Login ObjLogin)
        {
            string ResponseMessage = "";
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName="@LoginId",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=200,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjLogin.LogID)?String.Empty:ObjLogin.LogID
                        },
                        new SqlParameter() {
                            ParameterName="@LoginDetail",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=200,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjLogin.LogInDetail)?String.Empty:ObjLogin.LogInDetail.ToUpper()
                        },
                        new SqlParameter() {
                            ParameterName="@Password",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjLogin.Password)?String.Empty:ObjLogin.Password
                        },
                        new SqlParameter() {
                            ParameterName="@MessageStatus",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Output
                        }
                    };

                Collection<SqlParameter[]> parameters = new Collection<SqlParameter[]>();
                parameters.Add(param);

                string queryString = "";
                queryString = "sp_Login @LoginId,@LoginDetail,@Password,@MessageStatus Out";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 3);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_Login @LoginId,@LoginDetail,@Password,@MessageStatus Out", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> ForgetPasswordDB(forgetPassword ObjforgetPassword)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName="@LoginId",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=200,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjforgetPassword.LogID)?String.Empty:ObjforgetPassword.LogID
                        },
                        new SqlParameter() {
                            ParameterName="@LoginDetail",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=200,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjforgetPassword.LogInDetail)?String.Empty:ObjforgetPassword.LogInDetail.ToUpper()
                        },
                        new SqlParameter() {
                            ParameterName="@Password",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(ObjforgetPassword.Password)?String.Empty:ObjforgetPassword.Password
                        },
                        new SqlParameter() {
                            ParameterName="@MessageStatus",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Output
                        }
                    };

                string queryString = "";
                queryString = "sp_LoginUpdate @LoginId,@LoginDetail,@Password,@MessageStatus Out";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 3);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_LoginUpdate @LoginId,@LoginDetail,@Password,@MessageStatus Out", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> RegisterDB(Authentication ObjAuthentication, string userID, int otp)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=userID
                    },
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=ObjAuthentication.UserName
                    },
                    new SqlParameter() {
                        ParameterName="@password",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=ObjAuthentication.password
                    },
                    new SqlParameter() {
                        ParameterName="@EmailID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=ObjAuthentication.EmailID
                    },
                    new SqlParameter() {
                        ParameterName="@PhoneNum",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=ObjAuthentication.PhoneNum
                    },
                    new SqlParameter() {
                        ParameterName="@CategoryID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value="1"
                    },
                    new SqlParameter() {
                        ParameterName="@OTP",
                        SqlDbType=System.Data.SqlDbType.Int,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=otp
                    },
                    new SqlParameter() {
                        ParameterName="@ResponseMessage",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=-1,
                        Direction=System.Data.ParameterDirection.Output,
                    }
                };

                string queryString = "";
                queryString = "sp_CreateUser @userID,@userName,@password,@EmailID,@PhoneNum,@CategoryID,@OTP,@ResponseMessage out";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 7);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_CreateUser @userID,@userName,@password,@EmailID,@PhoneNum,@CategoryID,@ResponseMessage out", param);
                //ResponseMessage = Convert.ToString(param[6].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<List<AddEvent>> GetEventsDB()
        {
            return await _IDBLayercs.ExecuteAsyncTableSql<AddEvent>("sp_GetAllEvents");

            //var _Context = new AuthenticationContext();
            //var events = _Context.Set<AddEvent>().FromSqlRaw("sp_GetAllEvents").ToList();
            //return events;
        }

        public async Task<List<DBBookingstatusResponse>> GetBookingStatusDB(Bookingstatus ObjBookingstatus)
        {
            var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value= string.IsNullOrEmpty(ObjBookingstatus.UserID)?String.Empty:ObjBookingstatus.UserID
                    },
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjBookingstatus.UserName)?String.Empty:ObjBookingstatus.UserName
                    },
                    new SqlParameter() {
                        ParameterName="@EmailID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjBookingstatus.Email)?String.Empty:ObjBookingstatus.Email
                    },
                    new SqlParameter() {
                        ParameterName="@PhoneNum",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjBookingstatus.PhoneNum)?String.Empty:ObjBookingstatus.PhoneNum
                    }
                };

            string query = "sp_CustBookingStatus @userID,@userName,@EmailID,@PhoneNum";
            var events = await _IDBLayercs.ExecuteAsyncTableProcedure<DBBookingstatusResponse>(param, query);
            return events;
            //var _Context = new AuthenticationContext();
            //var events = _Context.Set<DBBookingstatusResponse>().FromSqlRaw("sp_CustBookingStatus @userID,@userName,@EmailID,@PhoneNum", param).ToList();
            //return events;
        }

        public async Task<string> BookingDB(Booking objBookingRequest, string BookingID, string TicketCategory, string NoofTickets)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName="@userName",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(objBookingRequest.UserName)?String.Empty:objBookingRequest.UserName
                        },
                        new SqlParameter() {
                            ParameterName="@BookingID",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=BookingID
                        },
                        new SqlParameter() {
                            ParameterName="@StatusInfo",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value="Booking Confirmed"
                        },
                        new SqlParameter() {
                            ParameterName="@EventID",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(objBookingRequest.EventID)?String.Empty:objBookingRequest.EventID
                        },
                        new SqlParameter() {
                            ParameterName="@SubEventID",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(objBookingRequest.SubEventID)?String.Empty:objBookingRequest.SubEventID
                        },
                        new SqlParameter() {
                            ParameterName="@TicketCategory",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=TicketCategory
                        },
                        new SqlParameter() {
                            ParameterName="@nNoofTickets",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=NoofTickets
                        },
                        new SqlParameter() {
                            ParameterName="@TotalAmount",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Input,
                            Value=string.IsNullOrEmpty(objBookingRequest.TotalAmount)?String.Empty:objBookingRequest.TotalAmount
                        },
                        new SqlParameter() {
                            ParameterName="@MessageStatus",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=100,
                            Direction=System.Data.ParameterDirection.Output
                        }
                    };

                string queryString = "";
                queryString = "sp_BookingConfirmation @userName,@BookingID,@StatusInfo,@EventID,@SubEventID,@TicketCategory,@nNoofTickets,@TotalAmount,@MessageStatus Out";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 8);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_BookingConfirmation @userName,@BookingID,@StatusInfo,@EventID,@SubEventID,@TicketCategory,@nNoofTickets,@TotalAmount,@MessageStatus Out", param);
                //ResponseMessage = Convert.ToString(param[8].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> CancelBookingDB(BookingCancel ObjBookingCancel)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=10,
                        Direction=System.Data.ParameterDirection.Input,
                        Value= string.IsNullOrEmpty(ObjBookingCancel.UserID)?String.Empty:ObjBookingCancel.UserID
                    },
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjBookingCancel.UserName)?String.Empty:ObjBookingCancel.UserName
                    },
                    new SqlParameter() {
                        ParameterName="@BookingID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=30,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjBookingCancel.BookingID)?String.Empty:ObjBookingCancel.BookingID
                    },
                    new SqlParameter() {
                            ParameterName="@MessageStatus",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Output
                        }
                };

                string queryString = "";
                queryString = "sp_CustBookingCancel @userID,@userName,@BookingID,@MessageStatus OUT";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 3);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_CustBookingCancel @userID,@userName,@BookingID,@MessageStatus OUT", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch(Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> SubmitforPartnerShipDB(Submitforpartnership ObjSubmitforpartnership, string RequestID)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=10,
                        Direction=System.Data.ParameterDirection.Input,
                        Value= string.IsNullOrEmpty(ObjSubmitforpartnership.UserID)?String.Empty:ObjSubmitforpartnership.UserID
                    },
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjSubmitforpartnership.UserName)?String.Empty:ObjSubmitforpartnership.UserName
                    },
                    new SqlParameter() {
                        ParameterName="@AadharNum",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=20,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjSubmitforpartnership.AadharNum)?String.Empty:ObjSubmitforpartnership.AadharNum
                    },
                    new SqlParameter() {
                        ParameterName="@AadharImange",
                        SqlDbType=System.Data.SqlDbType.Image,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjSubmitforpartnership.AadharPic)? Convert.FromBase64String(string.Empty): Convert.FromBase64String(ObjSubmitforpartnership.AadharPic)
                    },
                    new SqlParameter() {
                        ParameterName="@AdditionalDocImage",
                        SqlDbType=System.Data.SqlDbType.Image,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjSubmitforpartnership.AddtionalDocument)?Convert.FromBase64String(string.Empty): Convert.FromBase64String(ObjSubmitforpartnership.AddtionalDocument)
                    },
                    new SqlParameter() {
                        ParameterName="@AddtionDocDetails",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjSubmitforpartnership.AddtionalDocumentDetail)?String.Empty:ObjSubmitforpartnership.AddtionalDocumentDetail
                    },
                    new SqlParameter() {
                        ParameterName="@BookingID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=30,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=RequestID
                    },
                    new SqlParameter() {
                            ParameterName="@ResponseMessage",
                            SqlDbType=System.Data.SqlDbType.VarChar,
                            Size=-1,
                            Direction=System.Data.ParameterDirection.Output
                        }
                };

                string queryString = "";
                queryString = "sp_SubmitforPartnerShip @UserID,@userName,@AadharNum,@AadharImange,@AdditionalDocImage,@AddtionDocDetails,@BookingID,@ResponseMessage OUT";
                
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 7);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_SubmitforPartnerShip @UserID,@userName,@AadharNum,@AadharImange,@AdditionalDocImage,@AddtionDocDetails,@BookingID,@ResponseMessage OUT", param);
                //ResponseMessage = Convert.ToString(param[7].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<List<PendingApproval>> GetPendingApprovalDB()
        {
            return await _IDBLayercs.ExecuteAsyncTableSql<PendingApproval>("sp_getPendingforApproval");
            //var _Context = new AuthenticationContext();
            //var events = _Context.Set<PendingApproval>().FromSqlRaw("sp_getPendingforApproval").ToList();
            //return events;
        }

        public async Task<string> ParnershipApprovalDB(PartnershipApproval objPartnershipApproval)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=10,
                        Direction=System.Data.ParameterDirection.Input,
                        Value= string.IsNullOrEmpty(objPartnershipApproval.UserID)?String.Empty:objPartnershipApproval.UserID
                    },
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(objPartnershipApproval.UserName)?String.Empty:objPartnershipApproval.UserName
                    },
                    new SqlParameter() {
                        ParameterName="@BookingID",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=30,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(objPartnershipApproval.RequestID)?String.Empty:objPartnershipApproval.RequestID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ResponseMessage",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = -1,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };
                string queryString = "";
                queryString = "sp_PartnershipApprovalupdate @UserID,@userName,@BookingID,@ResponseMessage OUT";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 3);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_PartnershipApprovalupdate @UserID,@userName,@BookingID,@ResponseMessage OUT", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> OTPGenerationDB(ObjOTPGenerationRequest ObjOTPGeneration, string otp)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjOTPGeneration.Username)?String.Empty:ObjOTPGeneration.Username
                    },
                    new SqlParameter() {
                        ParameterName="@OTP",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=10,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=otp
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ResponseMessage",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = -1,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };
                string queryString = "";
                queryString = "Sp_OTPGeneration @userName,@OTP,@ResponseMessage OUT";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 2);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_PartnershipApprovalupdate @UserID,@userName,@BookingID,@ResponseMessage OUT", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }

        public async Task<string> OTPVerificationDB(OTPVerificationRequest ObjOTPVerificationRequest)
        {
            string ResponseMessage = "";
            try
            {
                var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName="@userName",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=100,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjOTPVerificationRequest.Username)?String.Empty:ObjOTPVerificationRequest.Username
                    },
                    new SqlParameter() {
                        ParameterName="@OTP",
                        SqlDbType=System.Data.SqlDbType.VarChar,
                        Size=10,
                        Direction=System.Data.ParameterDirection.Input,
                        Value=string.IsNullOrEmpty(ObjOTPVerificationRequest.otp)?String.Empty:ObjOTPVerificationRequest.otp
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ResponseMessage",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = -1,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };
                string queryString = "";
                queryString = "Sp_OTPVerification @userName,@OTP,@ResponseMessage OUT";
                ResponseMessage = await _IDBLayercs.ExecuteAsyncSql(param, queryString, 2);

                //var _Context = new AuthenticationContext();

                //int affectedRows = await _Context.Database.ExecuteSqlRawAsync("sp_PartnershipApprovalupdate @UserID,@userName,@BookingID,@ResponseMessage OUT", param);
                //ResponseMessage = Convert.ToString(param[3].Value);
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message.ToString();
            }
            return ResponseMessage;
        }
    }
}
