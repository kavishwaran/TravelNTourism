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
    public class HotelController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public HotelController(IUserRepository userrepository, 
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _userRepo = userrepository;
            _hotelRepository = hotelRepository;
           _mapper = mapper;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("CreateHotel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHotel([FromBody] RestaurantDto CreateDto)
        {
            try
            {

                if (await _hotelRepository.GetAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels are Created with that name");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                CreateDto.IsActive = "Y";
                Restaurant restaurant = _mapper.Map<Restaurant>(CreateDto);
                 

                await _hotelRepository.CreateAsync(restaurant);
                _response.Result = _mapper.Map<RestaurantDto>(restaurant);
                _response.StatusCode = HttpStatusCode.Created;

               // return CreatedAtRoute("GetVilla", new { id = restaurant.Id }, _response);
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
        [HttpGet("GetAllHotels")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllHotels()
        {
            try
            { 
                IEnumerable<Restaurant> restaurantList = await _hotelRepository.GetAllAsync(a=> a.IsActive == "Y");
                _response.Result = _mapper.Map<List<RestaurantDto>>(restaurantList);
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
        [HttpPost("UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateHotel([FromBody] RestaurantUpdateDto UpdateDto)
        {
            try
            {
                if (await _hotelRepository.GetAsync(a=> a.Id == UpdateDto.Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                 _hotelRepository.UpdateAsync(UpdateDto);
                await _hotelRepository.SaveAsync();
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

        [AllowAnonymous]
        [HttpPost("DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteHotel([FromHeader] int Id)
        {
            try
            {
                if (await _hotelRepository.GetAsync(a => a.Id == Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                _hotelRepository.DeleteAsync(Id);
               await _hotelRepository.SaveAsync();
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
