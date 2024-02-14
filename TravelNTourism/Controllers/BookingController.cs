using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNTourism.Data;
using TravelNTourism.Model;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public BookingController(IUserRepository userrepository,   
            IBookingRepository bookingRepo,
            IMapper mapper)
        {
            _userRepo = userrepository;
            _bookingRepo = bookingRepo;
            _mapper = mapper;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("CreateBooking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateBooking([FromBody] BookingDto CreateDto)
        {
            try
            {  
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
               // CreateDto.IsActive = "Y";
                Booking booking = _mapper.Map<Booking>(CreateDto); 

                await _bookingRepo.CreateAsync(booking);
                _response.Result = _mapper.Map<BookingDto>(booking);
                _response.StatusCode = HttpStatusCode.Created; 
                return _response;
            }
            catch (Exception ex)
            { 
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [AllowAnonymous] 
        [HttpGet("GetAllBookings")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllBookings()
        {
            try
            { 
                IEnumerable<Booking> bookings = await _bookingRepo.GetAllAsync();
                _response.Result = _mapper.Map<List<Booking>>(bookings);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [AllowAnonymous]
        [HttpPost("UpdateBooking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateBooking([FromBody] BookingUpdateDto UpdateDto)
        {
            try
            {
                if (await _bookingRepo.GetAsync(a=> a.Id == UpdateDto.Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                _bookingRepo.UpdateAsync(UpdateDto);
                await _bookingRepo.SaveAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;   
                return _response;
            }
            catch (Exception ex)
            { 
                _response.IsSuccess = false;
                _response.ErrorMessages
                = new List<string>() { ex.ToString() };
            }
            return _response;
        }

       
    }
}
