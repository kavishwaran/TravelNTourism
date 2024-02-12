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
    public class GuideController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IGuideRepository _guideRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public GuideController(IUserRepository userrepository,  
            IGuideRepository guideRepo,
            IMapper mapper)
        {
            _userRepo = userrepository;
            _guideRepo = guideRepo;
            _mapper = mapper;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("CreateGuide")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateGuide([FromBody] GuideDto CreateDto)
        {
            try
            {  
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                CreateDto.IsActive = "Y";
                Guide guide = _mapper.Map<Guide>(CreateDto); 

                await _guideRepo.CreateAsync(guide);
                _response.Result = _mapper.Map<GuideDto>(guide);
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
        [HttpGet("GetAllGuides")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllGuides()
        {
            try
            { 
                IEnumerable<Guide> guides = await _guideRepo.GetAllAsync(a=> a.IsActive == "Y");
                _response.Result = _mapper.Map<List<Guide>>(guides);
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
        [HttpPost("UpdateGuide")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateGuide([FromBody] GuideUpdateDto UpdateDto)
        {
            try
            {
                if (await _guideRepo.GetAsync(a=> a.Id == UpdateDto.Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                 _guideRepo.UpdateAsync(UpdateDto);
                await _guideRepo.SaveAsync();
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
        [HttpPost("DeleteGuide")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteGuide([FromHeader] int Id)
        {
            try
            {
                if (await _guideRepo.GetAsync(a => a.Id == Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
                    return BadRequest(ModelState);
                }
                _guideRepo.DeleteAsync(Id);
               await _guideRepo.SaveAsync();
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
