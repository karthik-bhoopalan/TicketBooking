using EventBooking.Data;
using EventBooking.DataBase;
using EventBooking.Modal;
using EventBooking.Service;
using Microsoft.Data.SqlClient;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventBooking.moq
{
    

    public class EventBookingRepositoryTest
    {
        private readonly EventBookingRepository _eventBookingRepository;
        private readonly Mock<IDBLayer> _eventDBMock = new Mock<IDBLayer>();

        private readonly IDBLayer _eventDBMock1 = Substitute.For<IDBLayer>();

        public EventBookingRepositoryTest()
        {
            _eventBookingRepository = new EventBookingRepository(_eventDBMock.Object);
        }


        [Fact]
        public async Task LogInDB_ShouldReturnSuccess_WhenPasswordValidforusername()
        {
            //Arrange
            Login ObjLogin = new Login();
            ObjLogin.LogID = "monica";
            ObjLogin.LogInDetail = "Username";
            ObjLogin.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Login Success";

            //var parameters = new SqlParameter[] {
            //            new SqlParameter() {
            //                ParameterName="@LoginId",
            //                SqlDbType=System.Data.SqlDbType.VarChar,
            //                Size=200,
            //                Direction=System.Data.ParameterDirection.Input,
            //                Value=string.IsNullOrEmpty(ObjLogin.LogID)?String.Empty:ObjLogin.LogID
            //            },
            //            new SqlParameter() {
            //                ParameterName="@LoginDetail",
            //                SqlDbType=System.Data.SqlDbType.VarChar,
            //                Size=200,
            //                Direction=System.Data.ParameterDirection.Input,
            //                Value=string.IsNullOrEmpty(ObjLogin.LogInDetail)?String.Empty:ObjLogin.LogInDetail.ToUpper()
            //            },
            //            new SqlParameter() {
            //                ParameterName="@Password",
            //                SqlDbType=System.Data.SqlDbType.VarChar,
            //                Size=-1,
            //                Direction=System.Data.ParameterDirection.Input,
            //                Value=string.IsNullOrEmpty(ObjLogin.Password)?String.Empty:ObjLogin.Password
            //            },
            //            new SqlParameter() {
            //                ParameterName="@MessageStatus",
            //                SqlDbType=System.Data.SqlDbType.VarChar,
            //                Size=-1,
            //                Direction=System.Data.ParameterDirection.Output
            //            }
            //        };

            string queryString = "";
            queryString = "sp_Login @LoginId,@LoginDetail,@Password,@MessageStatus Out";
            int outparameterValue = 3;

            //Collection<SqlParameter[]> para = new Collection<SqlParameter[]>();
            //para.Add(parameters);

            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString, outparameterValue)).ReturnsAsync("Login Success");
            //_eventDBMock1.ExecuteAsyncSql(parameters, queryString, outparameterValue).Returns("Login Success");

            //Act
            string customer = await _eventBookingRepository.LogInDB(ObjLogin);

            //Assert
            Assert.Equal(objRes.ResponseMessage, customer);
        }


        [Fact]
        public async Task ForgetPassword_ShouldReturnSuccess_WhenPasswordValidforusername()
        {
            //Arrange
            forgetPassword ObjforgetPassword = new forgetPassword();
            ObjforgetPassword.LogID = "monica";
            ObjforgetPassword.LogInDetail = "Username";
            ObjforgetPassword.Password = "monica";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Password Changed Successfully";

            string queryString = "";
            queryString = "sp_LoginUpdate @LoginId,@LoginDetail,@Password,@MessageStatus Out";

            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString, 3)).ReturnsAsync("Password Changed Successfully");

            //Act
            var customer = await _eventBookingRepository.ForgetPasswordDB(ObjforgetPassword);

            //Assert
            Assert.Equal(objRes.ResponseMessage, customer);
        }

        [Fact]
        public async Task register_ShouldReturnValidMessage()
        {
            //Arrange
            Authentication Obj = new Authentication();
            string name = "monica", emailID = "monica@123", phonenum = "1234567891";

            Obj.UserName = name;
            Obj.EmailID = emailID;
            Obj.PhoneNum = phonenum;
            Obj.password = "monica";
            Obj.CategoryID = "1";

            Random objRan = new Random();
            string userID = Convert.ToString(objRan.Next(0000, 9999));

            AuthenticationResponse objRes = new AuthenticationResponse();
            objRes.UserID = userID;
            objRes.ResponseMessage = "Customer Created Successufully";
            objRes.PhoneNum = phonenum;
            objRes.UserName = name;
            objRes.EmailID = emailID;

            string queryString = "";
            queryString = "sp_CreateUser @userID,@userName,@password,@EmailID,@PhoneNum,@CategoryID,@ResponseMessage out";

            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString, 6)).ReturnsAsync("Customer Created Successufully");

            //Act

            var customer = await _eventBookingRepository.RegisterDB(Obj, userID);

            //Assert

            Assert.Equal(objRes.ResponseMessage, customer);
        }


        [Fact]
        public async Task GetEvents_ShouldReturnEventsifpresent()
        {
            //Arrange

            AddEventResponse ObjAddEventResponse = new AddEventResponse();

            Response ObjResponse = new Response();
            ObjResponse.EventID = "1";
            ObjResponse.EventName = "IPL";
            ObjResponse.EventStatus = "Open";
            ObjResponse.EventTimings = "7 PM";
            ObjAddEventResponse.response.Add(ObjResponse);
            ObjAddEventResponse.ResponseMessage = "Success";

            AddEventResponse ObjAddEventResponse1 = new AddEventResponse();

            List<AddEvent> obj = new List<AddEvent>();
            AddEvent addEvent = new AddEvent();

            addEvent.EventID = "1";
            addEvent.EventName = "IPL";
            addEvent.EventStatus = "Open";
            addEvent.EventTimings = "7 PM";
            obj.Add(addEvent);

            _eventDBMock.Setup(x => x.ExecuteAsyncTableSql<AddEvent>("sp_GetAllEvents")).ReturnsAsync(obj);

            //Act
            var customer = await _eventBookingRepository.GetEventsDB();


            //Assert

            Assert.NotNull(customer);

        }

        [Fact]
        public async Task GetBookingStatus_ShouldReturnBookingStatusifpresent()
        {
            //Arrange
            string name = "monica", emailID = "", phonenum = "";


            Bookingstatus ObjBookingstatus = new Bookingstatus();
            ObjBookingstatus.UserID = "123";
            ObjBookingstatus.UserName = name;
            ObjBookingstatus.Email = emailID;
            ObjBookingstatus.PhoneNum = phonenum;

            statusResponse ObjResponse = new statusResponse();
            ObjResponse.EventID = "1";
            ObjResponse.EventName = "IPL";
            ObjResponse.EventStatus = "Open";
            ObjResponse.EventTimings = "7 PM";
            ObjResponse.EventDetails = "MI vs CSK";
            ObjResponse.UserID = "222";
            ObjResponse.BookingID = "222";
            ObjResponse.Status = "Booking Confirmed";

            BookingstatusResponse objres = new BookingstatusResponse();

            objres.response.Add(ObjResponse);
            objres.ResponseMessage = "Booking Status found successfully";

            List<DBBookingstatusResponse> obj = new List<DBBookingstatusResponse>();
            DBBookingstatusResponse objresp = new DBBookingstatusResponse();
            objresp.EventID = "1";
            objresp.EventName = "IPL";
            objresp.EventStatus = "Open";
            objresp.EventTimings = "7 PM";
            objresp.EventDetails = "MI vs CSK";
            objresp.UserID = "222";
            objresp.BookingID = "222";
            objresp.Status = "Booking Confirmed";
            obj.Add(objresp);

            string query = "sp_CustBookingStatus @userID,@userName,@EmailID,@PhoneNum";
            _eventDBMock.Setup(x => x.ExecuteAsyncTableProcedure<DBBookingstatusResponse>(It.IsAny<SqlParameter[]>(), query)).ReturnsAsync(obj);


            //Act
            var customer = await _eventBookingRepository.GetBookingStatusDB(ObjBookingstatus);


            //Assert

            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Booking_ShouldCreateUser_ifsuccess()
        {
            //Arrange
            string userid = "222", userName = "monica", subEventID = "2", TotalAmt = "12000", eventID = "1";
            string BookingID = "", NoofTickets = "2", TicketCategory = "1";

            Random ObjRan = new Random();

            BookingID = Convert.ToString(ObjRan.Next(1000000, 99999999));

            Booking objBooking = new Booking();
            objBooking.UserID = userid;
            objBooking.UserName = userName;
            objBooking.EventID = eventID;
            objBooking.SubEventID = subEventID;
            objBooking.TotalAmount = TotalAmt;

            BookingRequest objBookingRequest = new BookingRequest();
            objBookingRequest.NoofTickets = NoofTickets;
            objBookingRequest.TicketCategory = TicketCategory;

            objBooking.Request.Add(objBookingRequest);

            string queryString = "";
            queryString = "sp_BookingConfirmation @userName,@BookingID,@StatusInfo,@EventID,@SubEventID,@TicketCategory,@nNoofTickets,@TotalAmount,@MessageStatus Out";

            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString, 8)).ReturnsAsync("Booking Confirmed");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.BookingID = BookingID;
            objBookingResponse.ResponseMessage = "Booking Confirmed";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _eventBookingRepository.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer);
        }


        [Fact]
        public async Task CancelBooking_TicketBookedshouldbecancelled()
        {
            //Arrange
            string userid = "222", userName = "monica";
            string BookingID = "354436";

            BookingCancel objBookingCancel = new BookingCancel();
            objBookingCancel.BookingID = BookingID;
            objBookingCancel.UserID = userid;
            objBookingCancel.UserName = userName;

            string queryString = "";
            queryString = "sp_CustBookingCancel @userID,@userName,@BookingID,@MessageStatus OUT";

            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString,3)).ReturnsAsync("Booking Cancelled");

            BookingCancelResponse objBookingCancelResponse = new BookingCancelResponse();
            objBookingCancelResponse.ResponseMessage = "Booking Cancelled";
            objBookingCancelResponse.UserID = userid;
            objBookingCancelResponse.UserName = userName;
            objBookingCancelResponse.BookingID = BookingID;
            //Act

            var customer = await _eventBookingRepository.CancelBookingDB(objBookingCancel);

            //Assert

            Assert.Equal(objBookingCancelResponse.ResponseMessage, customer);
        }

        [Fact]
        public async Task SubmitforPartnerShip_submitforApprovalofpartnership()
        {
            //Arrange
            string userid = "222", userName = "monica";

            string RequestID = "";
            Random ObjRan = new Random();

            RequestID = Convert.ToString(ObjRan.Next(1000000, 99999999));

            Submitforpartnership objSubmitforpartnership = new Submitforpartnership();
            objSubmitforpartnership.UserID = userid;
            objSubmitforpartnership.UserName = userName;
            objSubmitforpartnership.AadharNum = "12341234";
            objSubmitforpartnership.AadharPic = "";
            objSubmitforpartnership.AddtionalDocument = "";
            objSubmitforpartnership.AddtionalDocumentDetail = "";

            string queryString = "";
            queryString = "sp_SubmitforPartnerShip @UserID,@userName,@AadharNum,@AadharImange,@AdditionalDocImage,@AddtionDocDetails,@BookingID,@ResponseMessage OUT";


            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(), queryString,7)).ReturnsAsync("Submitted User for ParnerShip");

            SubmitforpartnershipResponse objSubmitforpartnershipResponse = new SubmitforpartnershipResponse();
            objSubmitforpartnershipResponse.PartnerShipRequestID = RequestID;
            objSubmitforpartnershipResponse.ResponseMessage = "Submitted User for ParnerShip";
            objSubmitforpartnershipResponse.UserID = userid;
            objSubmitforpartnershipResponse.UserName = userName;
            //Act

            var customer = await _eventBookingRepository.SubmitforPartnerShipDB(objSubmitforpartnership, RequestID);

            //Assert

            Assert.Equal(objSubmitforpartnershipResponse.ResponseMessage, customer);
        }


        [Fact]
        public async Task ParnershipApproval_Categoryofuserchanges()
        {
            //Arrange
            string userid = "222", userName = "monica";

            string RequestID = "";
            Random ObjRan = new Random();

            RequestID = Convert.ToString(ObjRan.Next(1000000, 99999999));

            PartnershipApproval objPartnershipApproval = new PartnershipApproval();
            objPartnershipApproval.RequestID = RequestID;
            objPartnershipApproval.UserID = userid;
            objPartnershipApproval.UserName = userName;

            string queryString = "";
            queryString = "sp_PartnershipApprovalupdate @UserID,@userName,@BookingID,@ResponseMessage OUT";
            _eventDBMock.Setup(x => x.ExecuteAsyncSql(It.IsAny<SqlParameter[]>(),queryString,3)).ReturnsAsync("User has been approved for partnership");

            PartnershipApprovalResponse objPartnershipApprovalResponse = new PartnershipApprovalResponse();
            objPartnershipApprovalResponse.UserID = userid;
            objPartnershipApprovalResponse.UserName = userName;
            objPartnershipApprovalResponse.RequestID = RequestID;
            objPartnershipApprovalResponse.ResponseMessage = "User has been approved for partnership";
            //Act

            var customer = await _eventBookingRepository.ParnershipApprovalDB(objPartnershipApproval);

            //Assert

            Assert.Equal(objPartnershipApprovalResponse.ResponseMessage, customer);
        }


        [Fact]
        public async Task GetPendingApproval_GetPending_Custforapproval()
        {
            //Arrange
            string userid = "222", userName = "monica";

            List<PendingApproval> objPenList = new List<PendingApproval>();


            PendingApproval objpen = new PendingApproval();
            objpen.UserID = userid;
            objpen.RequestID = "";
            objpen.AadharNum = "";
            objpen.AadharImage = Convert.FromBase64String("");
            objpen.AddtionDocDetails = "";
            objpen.AdditionalDocImage = Convert.FromBase64String("");

            objPenList.Add(objpen);


            _eventDBMock.Setup(x => x.ExecuteAsyncTableSql<PendingApproval>("sp_getPendingforApproval")).ReturnsAsync(objPenList);

            PendingApprovalResponse oblpenResponse = new PendingApprovalResponse();
            oblpenResponse.ResponseMessage = "Sucess";

            ApprovalResponse objApprovalResponse = new ApprovalResponse();
            objApprovalResponse.UserID = userid;
            objApprovalResponse.RequestID = "";
            objApprovalResponse.AadharNum = "";
            objApprovalResponse.AadharImage = ("");
            objApprovalResponse.AddtionDocDetails = "";
            objApprovalResponse.AdditionalDocImage = ("");
            oblpenResponse.response.Add(objApprovalResponse);
            //Act

            var customer = await _eventBookingRepository.GetPendingApprovalDB();

            //Assert

            Assert.NotNull(customer);
        }
    }
}
