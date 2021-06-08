using EventBooking.DataBase;
using EventBooking.Modal;
using EventBooking.Service;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EventBooking.moq
{
    public class BooksRepositoryTest
    {
        private readonly EventBookingService _EventBookingService;
        private readonly Mock<IEventBookingRepository> _eventRepoMock = new Mock<IEventBookingRepository>();
        private readonly Mock<ILoggerService> _LoggingMock = new Mock<ILoggerService>();

        public BooksRepositoryTest()
        {
            _EventBookingService = new EventBookingService(_eventRepoMock.Object, _LoggingMock.Object);
        } 

        [Fact]
        public async Task LogIn_ShouldReturnSuccess_WhenPasswordValidforusername()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "monica";
            Obj.LogInDetail = "Username";
            Obj.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Login Success";

            _eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnSuccess_WhenPasswordValidforEmail()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "monica@123";
            Obj.LogInDetail = "email";
            Obj.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica@123";
            objRes.ResponseMessage = "Login Success";

            _eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnSuccess_WhenPasswordValidforphonenumber()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "1234567891";
            Obj.LogInDetail = "PHONENUM";
            Obj.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "1234567891";
            objRes.ResponseMessage = "Login Success";

            _eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnInvalidMessage_WhenWrongPassword()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "1234567891";
            Obj.LogInDetail = "email";
            Obj.Password = "monic";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "1234567891";
            objRes.ResponseMessage = "Invalid PhoneNum or Password";

            _eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Invalid PhoneNum or Password");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnInvalidMessage_IFLogIDnotpresent()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "";
            Obj.LogInDetail = "email";
            Obj.Password = "monic";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "";
            objRes.ResponseMessage = "Please enter UserName/Email ID/ Phone Num";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnInvalidMessage_IFLogINDetailnotpresent()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "monica";
            Obj.LogInDetail = "";
            Obj.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Please Select whether the Login ID is UserName/Email ID/ Phone Num";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnInvalidMessage_IFLogINDetailnotvalid()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "monica";
            Obj.LogInDetail = "dfg";
            Obj.Password = "monica";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Login Method is invalid, Please Try again!!!";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task LogIn_ShouldReturnInvalidMessage_IFPasswordnotentered()
        {
            //Arrange
            Login Obj = new Login();
            Obj.LogID = "monica";
            Obj.LogInDetail = "username";
            Obj.Password = "";

            LoginResponse objRes = new LoginResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Please enter Password";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.LogIn(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }


        [Fact]
        public async Task ForgetPassword_ShouldReturnSuccess_WhenPasswordValidforusername()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "monica";
            Obj.LogInDetail = "Username";
            Obj.Password = "monica";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Password Changed Successfully";

            _eventRepoMock.Setup(x => x.ForgetPasswordDB(Obj)).ReturnsAsync("Password Changed Successfully");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }


        [Fact]
        public async Task ForgetPassword_ShouldReturnSuccess_WhenPasswordInValidforusername()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "monic";
            Obj.LogInDetail = "Username";
            Obj.Password = "monica";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monic";
            objRes.ResponseMessage = "Invalid UserName";

            _eventRepoMock.Setup(x => x.ForgetPasswordDB(Obj)).ReturnsAsync("Invalid UserName");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }


        [Fact]
        public async Task ForgetPassword_ShouldReturnInvalidMessage_IFLogIDnotpresent()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "";
            Obj.LogInDetail = "email";
            Obj.Password = "monic";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "";
            objRes.ResponseMessage = "Please enter UserName/Email ID/ Phone Num";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task ForgetPassword_ShouldReturnInvalidMessage_IFLogINDetailnotpresent()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "monica";
            Obj.LogInDetail = "";
            Obj.Password = "monica";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Please Select whether the Login ID is UserName/Email ID/ Phone Num";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task ForgetPassword_ShouldReturnInvalidMessage_IFLogINDetailnotvalid()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "monica";
            Obj.LogInDetail = "dfg";
            Obj.Password = "monica";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Login Method is invalid, Please Try again!!!";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
        }

        [Fact]
        public async Task ForgetPassword_ShouldReturnInvalidMessage_IFPasswordnotentered()
        {
            //Arrange
            forgetPassword Obj = new forgetPassword();
            Obj.LogID = "monica";
            Obj.LogInDetail = "username";
            Obj.Password = "";

            ForgetPasswordResponse objRes = new ForgetPasswordResponse();
            objRes.LogID = "monica";
            objRes.ResponseMessage = "Please enter Password";

            //_eventRepoMock.Setup(x => x.LogInDB(Obj)).ReturnsAsync("Login Success");

            //Act
            var customer = await _EventBookingService.ForgetPassword(Obj);

            //Assert
            Assert.Equal(objRes.LogID, customer.LogID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
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

            _eventRepoMock.Setup(x => x.RegisterDB(Obj, userID)).ReturnsAsync("Customer Created Successufully");

            //Act

            var customer = await _EventBookingService.Register(Obj, userID);

            //Assert

            Assert.Equal(userID, customer.UserID);
            Assert.Equal(objRes.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objRes.UserName, customer.UserName);
            Assert.Equal(objRes.EmailID, customer.EmailID);
            Assert.Equal(objRes.PhoneNum, customer.PhoneNum);
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

            _eventRepoMock.Setup(x => x.GetEventsDB()).ReturnsAsync(obj);

            //Act
            var customer = await _EventBookingService.GetEvents();


            //Assert
            
            Assert.Equal(ObjAddEventResponse.ResponseMessage, customer.ResponseMessage);

            for(int i = 0;i< customer.response.Count;i++)
            {
                Assert.Equal(ObjAddEventResponse.response[i].EventID, customer.response[i].EventID);
                Assert.Equal(ObjAddEventResponse.response[i].EventName, customer.response[i].EventName);
                Assert.Equal(ObjAddEventResponse.response[i].EventStatus, customer.response[i].EventStatus);
                Assert.Equal(ObjAddEventResponse.response[i].EventTimings, customer.response[i].EventTimings);
            }
            
        }


        [Fact]
        public async Task GetEvents_ShouldReturnvalidmessage_ifnoeventpresent()
        {
            //Arrange


            AddEventResponse ObjAddEventResponse = new AddEventResponse();

            Response ObjResponse = new Response();
            ObjAddEventResponse.ResponseMessage = "No events available";

            AddEventResponse ObjAddEventResponse1 = new AddEventResponse();

            List<AddEvent> obj = new List<AddEvent>();
            AddEvent addEvent = new AddEvent();

            _eventRepoMock.Setup(x => x.GetEventsDB()).ReturnsAsync(obj);

            //Act
            var customer = await _EventBookingService.GetEvents();


            //Assert

            Assert.Equal(ObjAddEventResponse.ResponseMessage, customer.ResponseMessage);

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
            

            _eventRepoMock.Setup(x => x.GetBookingStatusDB(ObjBookingstatus)).ReturnsAsync(obj);


            //Act
            var customer = await _EventBookingService.GetBookingStatus(ObjBookingstatus);


            //Assert

            Assert.Equal(objres.ResponseMessage, customer.ResponseMessage);

            for (int i = 0; i < customer.response.Count; i++)
            {
                Assert.Equal(objres.response[i].EventID, customer.response[i].EventID);
                Assert.Equal(objres.response[i].EventName, customer.response[i].EventName);
                Assert.Equal(objres.response[i].EventStatus, customer.response[i].EventStatus);
                Assert.Equal(objres.response[i].EventTimings, customer.response[i].EventTimings);

                Assert.Equal(objres.response[i].UserID, customer.response[i].UserID);
                Assert.Equal(objres.response[i].BookingID, customer.response[i].BookingID);
                Assert.Equal(objres.response[i].EventDetails, customer.response[i].EventDetails);
                Assert.Equal(objres.response[i].Status, customer.response[i].Status);
            }
        }

        [Fact]
        public async Task GetBookingStatus_ShouldReturnValidmessage_ifnoBookingfound()
        {
            //Arrange
            string name = "monica", emailID = "", phonenum = "";


            Bookingstatus ObjBookingstatus = new Bookingstatus();
            ObjBookingstatus.UserID = "123";
            ObjBookingstatus.UserName = name;
            ObjBookingstatus.Email = emailID;
            ObjBookingstatus.PhoneNum = phonenum;

            BookingstatusResponse objres = new BookingstatusResponse();
            objres.ResponseMessage = "No Bookings found";

            List<DBBookingstatusResponse> obj = new List<DBBookingstatusResponse>();

            _eventRepoMock.Setup(x => x.GetBookingStatusDB(ObjBookingstatus)).ReturnsAsync(obj);


            //Act
            var customer = await _EventBookingService.GetBookingStatus(ObjBookingstatus);


            //Assert

            Assert.Equal(objres.ResponseMessage, customer.ResponseMessage);
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

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets)).ReturnsAsync("Booking Confirmed");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.BookingID = BookingID;
            objBookingResponse.ResponseMessage = "Booking Confirmed";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.BookingID, customer.BookingID);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
        }

        [Fact]
        public async Task Booking_ShouldCreateUser_ifsuccesswithmorethan1Category()
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
            objBooking.Request.Add(objBookingRequest);

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("Booking Confirmed");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.BookingID = BookingID;
            objBookingResponse.ResponseMessage = "Booking Confirmed";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.BookingID, customer.BookingID);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
        }

        [Fact]
        public async Task Booking_ShouldnotCreateUser_EventIDnotsent()
        {
            //Arrange
            string userid = "222", userName = "monica", subEventID = "2", TotalAmt = "12000", eventID = ""; 
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

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets)).ReturnsAsync("Event not selected, please try again!!!");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.ResponseMessage = "Event not selected, please try again!!!";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
        }

        [Fact]
        public async Task Booking_ShouldnotCreateUser_Matchnotsent()
        {
            //Arrange
            string userid = "222", userName = "monica", subEventID = "", TotalAmt = "12000", eventID = "1";
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

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets)).ReturnsAsync("Match not selected, please try again!!!");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.ResponseMessage = "Match not selected, please try again!!!";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
        }

        [Fact]
        public async Task Booking_ShouldnotCreateUser_TotalAmountToBePaidnotSent()
        {
            //Arrange
            string userid = "222", userName = "monica", subEventID = "2", TotalAmt = "", eventID = "1";
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

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets)).ReturnsAsync("TotalAmount not generated, please try again!!!");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.ResponseMessage = "TotalAmount not generated, please try again!!!";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
        }

        [Fact]
        public async Task Booking_ShouldnotCreateUser_TicketCategorynotSent()
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

            _eventRepoMock.Setup(x => x.BookingDB(objBooking, BookingID, TicketCategory, NoofTickets)).ReturnsAsync("Ticket Category not selected");

            BookingResponse objBookingResponse = new BookingResponse();
            objBookingResponse.ResponseMessage = "Ticket Category not selected";
            objBookingResponse.UserID = userid;
            objBookingResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.Booking(objBooking, BookingID);

            //Assert

            Assert.Equal(objBookingResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingResponse.UserID, customer.UserID);
            Assert.Equal(objBookingResponse.UserName, customer.UserName);
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

            _eventRepoMock.Setup(x => x.CancelBookingDB(objBookingCancel)).ReturnsAsync("Booking Cancelled");

            BookingCancelResponse objBookingCancelResponse = new BookingCancelResponse();
            objBookingCancelResponse.ResponseMessage = "Booking Cancelled";
            objBookingCancelResponse.UserID = userid;
            objBookingCancelResponse.UserName = userName;
            objBookingCancelResponse.BookingID = BookingID;
            //Act

            var customer = await _EventBookingService.CancelBooking(objBookingCancel);

            //Assert

            Assert.Equal(objBookingCancelResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objBookingCancelResponse.UserID, customer.UserID);
            Assert.Equal(objBookingCancelResponse.UserName, customer.UserName);
            Assert.Equal(objBookingCancelResponse.BookingID, customer.BookingID);
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


            _eventRepoMock.Setup(x => x.SubmitforPartnerShipDB(objSubmitforpartnership, RequestID)).ReturnsAsync("Submitted User for ParnerShip");

            SubmitforpartnershipResponse objSubmitforpartnershipResponse = new SubmitforpartnershipResponse();
            objSubmitforpartnershipResponse.PartnerShipRequestID = RequestID;
            objSubmitforpartnershipResponse.ResponseMessage = "Submitted User for ParnerShip";
            objSubmitforpartnershipResponse.UserID = userid;
            objSubmitforpartnershipResponse.UserName = userName;
            //Act

            var customer = await _EventBookingService.SubmitforPartnerShip(objSubmitforpartnership, RequestID);

            //Assert

            Assert.Equal(objSubmitforpartnershipResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objSubmitforpartnershipResponse.UserID, customer.UserID);
            Assert.Equal(objSubmitforpartnershipResponse.UserName, customer.UserName);
            Assert.Equal(objSubmitforpartnershipResponse.PartnerShipRequestID, customer.PartnerShipRequestID);
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


            _eventRepoMock.Setup(x => x.ParnershipApprovalDB(objPartnershipApproval)).ReturnsAsync("User has been approved for partnership");

            PartnershipApprovalResponse objPartnershipApprovalResponse = new PartnershipApprovalResponse();
            objPartnershipApprovalResponse.UserID = userid;
            objPartnershipApprovalResponse.UserName = userName;
            objPartnershipApprovalResponse.RequestID = RequestID;
            objPartnershipApprovalResponse.ResponseMessage = "User has been approved for partnership";
            //Act

            var customer = await _EventBookingService.ParnershipApproval(objPartnershipApproval);

            //Assert

            Assert.Equal(objPartnershipApprovalResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(objPartnershipApprovalResponse.UserID, customer.UserID);
            Assert.Equal(objPartnershipApprovalResponse.UserName, customer.UserName);
            Assert.Equal(objPartnershipApprovalResponse.RequestID, customer.RequestID);
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


            _eventRepoMock.Setup(x => x.GetPendingApprovalDB()).ReturnsAsync(objPenList);

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

            var customer = await _EventBookingService.GetPendingApproval();

            //Assert

            Assert.Equal(oblpenResponse.ResponseMessage, customer.ResponseMessage);
            Assert.Equal(oblpenResponse.response[0].UserID, customer.response[0].UserID);
            Assert.Equal(oblpenResponse.response[0].Status, customer.response[0].Status);
            Assert.Equal(oblpenResponse.response[0].AadharImage, customer.response[0].AadharImage);
            Assert.Equal(oblpenResponse.response[0].AadharNum, customer.response[0].AadharNum);
            Assert.Equal(oblpenResponse.response[0].AdditionalDocImage, customer.response[0].AdditionalDocImage);
            Assert.Equal(oblpenResponse.response[0].AddtionDocDetails, customer.response[0].AddtionDocDetails);
            Assert.Equal(oblpenResponse.response[0].RequestID, customer.response[0].RequestID);
        }


        [Fact]
        public async Task GetPendingApproval_validmessagetobeshown_ifZeroPendingAvalable()
        {
            //Arrange
            string userid = "222", userName = "monica";

            List<PendingApproval> objPenList = new List<PendingApproval>();


            _eventRepoMock.Setup(x => x.GetPendingApprovalDB()).ReturnsAsync(objPenList);

            PendingApprovalResponse oblpenResponse = new PendingApprovalResponse();
            oblpenResponse.ResponseMessage = "No new Reqests found for Customer Partnership Approval";
            //Act

            var customer = await _EventBookingService.GetPendingApproval();

            //Assert

            Assert.Equal(oblpenResponse.ResponseMessage, customer.ResponseMessage);
        }
    }
}
