using AutoMapper;
using EventBooking.Controllers;
using EventBooking.Modal;
using EventBooking.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventBooking.moq
{
    public class EventBookingTest
    {
        private readonly EventBookingController _EventBookingController;
        private readonly Mock<IEventBookingService> _eventBookingService = new Mock<IEventBookingService>();
        private readonly Mock<IMapper> _eventMapper = new Mock<IMapper>();

        public EventBookingTest()
        {
            _EventBookingController = new EventBookingController(_eventBookingService.Object, _eventMapper.Object);
        }

        [Fact]
        public async Task LogIn_ShouldReturnObject_WhenPasswordValidforusername()
        {
            //Arrange
            Login ObjLogin = new Login();
            ObjLogin.LogID = "monica";
            ObjLogin.LogInDetail = "Username";
            ObjLogin.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Login Success";

            _eventBookingService.Setup(x => x.LogIn(ObjLogin)).ReturnsAsync(objRes);

            //Act
            var customer = await _EventBookingController.LogIn(ObjLogin);

            //Assert
            Assert.NotNull(customer);

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

            _eventBookingService.Setup(x => x.ForgetPassword(ObjforgetPassword)).ReturnsAsync(objRes);

            //Act
            var customer = await _EventBookingController.ForgetPassword(ObjforgetPassword);

            //Assert
            Assert.NotNull(customer);
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

            _eventBookingService.Setup(x => x.Register(Obj, userID)).ReturnsAsync(objRes);

            //Act
            var customer = await _EventBookingController.Register(Obj);

            //Assert
            Assert.NotNull(customer);
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

            _eventBookingService.Setup(x => x.GetEvents()).ReturnsAsync(ObjAddEventResponse);

            //Act
            var customer = await _EventBookingController.GetEvents();

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

            _eventBookingService.Setup(x => x.GetBookingStatus(ObjBookingstatus)).ReturnsAsync(objres);

            //Act
            var customer = await _EventBookingController.GetBookingStatus(ObjBookingstatus);

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

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.BookingID = BookingID;
            objBookingResponse.ResponseMessage = "Booking Confirmed";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;

            _eventBookingService.Setup(x => x.Booking(objBooking, BookingID)).ReturnsAsync(objBookingResponse);

            //Act
            var customer = await _EventBookingController.Booking(objBooking);

            //Assert
            Assert.NotNull(customer);
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

            BookingCancelResponse objBookingCancelResponse = new BookingCancelResponse();
            objBookingCancelResponse.ResponseMessage = "Booking Cancelled";
            objBookingCancelResponse.UserID = userid;
            objBookingCancelResponse.UserName = userName;
            objBookingCancelResponse.BookingID = BookingID;

            _eventBookingService.Setup(x => x.CancelBooking(objBookingCancel)).ReturnsAsync(objBookingCancelResponse);

            //Act
            var customer = await _EventBookingController.CancelBooking(objBookingCancel);

            //Assert
            Assert.NotNull(customer);
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

            SubmitforpartnershipResponse objSubmitforpartnershipResponse = new SubmitforpartnershipResponse();
            objSubmitforpartnershipResponse.PartnerShipRequestID = RequestID;
            objSubmitforpartnershipResponse.ResponseMessage = "Submitted User for ParnerShip";
            objSubmitforpartnershipResponse.UserID = userid;
            objSubmitforpartnershipResponse.UserName = userName;

            _eventBookingService.Setup(x => x.SubmitforPartnerShip(objSubmitforpartnership, RequestID)).ReturnsAsync(objSubmitforpartnershipResponse);

            //Act
            var customer = await _EventBookingController.SubmitforPartnerShip(objSubmitforpartnership);

            //Assert
            Assert.NotNull(customer);
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

            PartnershipApprovalResponse objPartnershipApprovalResponse = new PartnershipApprovalResponse();
            objPartnershipApprovalResponse.UserID = userid;
            objPartnershipApprovalResponse.UserName = userName;
            objPartnershipApprovalResponse.RequestID = RequestID;
            objPartnershipApprovalResponse.ResponseMessage = "User has been approved for partnership";

            _eventBookingService.Setup(x => x.ParnershipApproval(objPartnershipApproval)).ReturnsAsync(objPartnershipApprovalResponse);

            //Act
            var customer = await _EventBookingController.ParnershipApproval(objPartnershipApproval);

            //Assert
            Assert.NotNull(customer);
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

            _eventBookingService.Setup(x => x.GetPendingApproval()).ReturnsAsync(oblpenResponse);

            //Act
            var customer = await _EventBookingController.GetPendingApproval();

            //Assert
            Assert.NotNull(customer);
        }
    }
}