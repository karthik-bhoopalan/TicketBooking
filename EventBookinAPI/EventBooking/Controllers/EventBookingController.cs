using AutoMapper;
using EventBooking.Data;
using EventBooking.Modal;
using EventBooking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventBookingController : Controller
    {
        //private readonly IEventBookingRepository _EventBookingRepository;
        private readonly IMapper _mapper;
        private readonly IEventBookingService _eventBookingService;

        public EventBookingController(IEventBookingService eventBookingService, IMapper mapper)
        {
            _eventBookingService = eventBookingService ?? throw new ArgumentNullException(nameof(eventBookingService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(Login ObjLogin)
        {
            var bookEntities = await _eventBookingService.LogIn(ObjLogin);
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(forgetPassword ObjforgetPassword)
        {
            var bookEntities = await _eventBookingService.ForgetPassword(ObjforgetPassword);
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Authentication ObjAuthentication)
        {
            Random objRan = new Random();
            string userID = Convert.ToString(objRan.Next(0000, 9999));

            var bookEntities = await _eventBookingService.Register(ObjAuthentication, userID);
            return Ok(bookEntities);
        }

        [HttpGet]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var bookEntities = await _eventBookingService.GetEvents();
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("GetBookingStatus")]
        public async Task<IActionResult> GetBookingStatus(Bookingstatus ObjBookingstatus)
        {
            var bookEntities = await _eventBookingService.GetBookingStatus(ObjBookingstatus);
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("Booking")]
        public async Task<IActionResult> Booking(Booking objBookingRequest)
        {
            string BookingID = "";
            Random ObjRan = new Random();
            BookingID = Convert.ToString(ObjRan.Next(1000000, 99999999));

            var bookEntities = await _eventBookingService.Booking(objBookingRequest, BookingID);
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("CancelBooking")]
        public async Task<IActionResult> CancelBooking(BookingCancel ObjBookingCancel)
        {
            var bookEntities = await _eventBookingService.CancelBooking(ObjBookingCancel);
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("SubmitforPartnerShip")]
        public async Task<IActionResult> SubmitforPartnerShip(Submitforpartnership ObjSubmitforpartnership)
        {
            string RequestID = "";
            Random ObjRan = new Random();

            RequestID = Convert.ToString(ObjRan.Next(1000000, 99999999));

            var bookEntities = await _eventBookingService.SubmitforPartnerShip(ObjSubmitforpartnership, RequestID);
            return Ok(bookEntities);
        }

        [HttpGet]
        [Route("GetPendingApproval")]
        public async Task<IActionResult> GetPendingApproval()
        {
            var bookEntities = await _eventBookingService.GetPendingApproval();
            return Ok(bookEntities);
        }

        [HttpPost]
        [Route("ParnershipApproval")]
        public async Task<IActionResult> ParnershipApproval(PartnershipApproval objPartnershipApproval)
        {
            var bookEntities = await _eventBookingService.ParnershipApproval(objPartnershipApproval);
            return Ok(bookEntities);
        }
    }
}
