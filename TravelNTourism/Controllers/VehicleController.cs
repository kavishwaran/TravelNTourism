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
    public class VehicleController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public VehicleController(IUserRepository userrepository,   
            IVehicleRepository vehicleRepo,
            IMapper mapper)
        {
            _userRepo = userrepository;
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("CreateVehicle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVehicle([FromBody] VehicleDto CreateDto)
        {
            try
            {  
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                CreateDto.IsActive = "Y";
                Vehicle vehicle = _mapper.Map<Vehicle>(CreateDto); 

                await _vehicleRepo.CreateAsync(vehicle);
                _response.Result = _mapper.Map<VehicleDto>(vehicle);
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
        [HttpGet("GetAllVehicle")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllVehicle()
        {
            try
            { 
                IEnumerable<Vehicle> vehicles = await _vehicleRepo.GetAllAsync(a=> a.IsActive == "Y");
                _response.Result = _mapper.Map<List<Vehicle>>(vehicles);
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
        [HttpPost("UpdateVehicle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateVehicle([FromBody] VehicleUpdateDto UpdateDto)
        {
            try
            {
                if (await _vehicleRepo.GetAsync(a=> a.Id == UpdateDto.Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                _vehicleRepo.UpdateAsync(UpdateDto);
                await _vehicleRepo.SaveAsync();
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
        [HttpPost("DeleteVehicle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteVehicle([FromHeader] int Id)
        {
            try
            {
                if (await _vehicleRepo.GetAsync(a => a.Id == Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                _vehicleRepo.DeleteAsync(Id);
               await _vehicleRepo.SaveAsync();
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
