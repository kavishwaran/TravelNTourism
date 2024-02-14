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
    public class PaymentController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public PaymentController(IUserRepository userrepository,   
            IPaymentRepository paymentRepo,
            IMapper mapper)
        {
            _userRepo = userrepository;
            _paymentRepo = paymentRepo;
            _mapper = mapper;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("CreatePayment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePayment([FromBody] PaymentDto CreateDto)
        {
            try
            {  
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                if (CreateDto.BookingId == 0)
                {
                    ModelState.AddModelError("ErrorMessages", "Booking Id is empty");
                    return BadRequest(ModelState);
                }
                Payment payment = _mapper.Map<Payment>(CreateDto);  
                await _paymentRepo.CreateAsync(payment);
                _response.Result = _mapper.Map<PaymentDto>(payment);
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
        [HttpGet("GetAllPayments")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllPayments()
        {
            try
            { 
                IEnumerable<Payment> payments = await _paymentRepo.GetAllAsync();
                _response.Result = _mapper.Map<List<Payment>>(payments);
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

        //[AllowAnonymous]
        //[HttpPost("UpdateGuide")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> UpdateGuide([FromBody] GuideUpdateDto UpdateDto)
        //{
        //    try
        //    {
        //        if (await _guideRepo.GetAsync(a=> a.Id == UpdateDto.Id) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
        //            return BadRequest(ModelState);
        //        }
        //         _guideRepo.UpdateAsync(UpdateDto);
        //        await _guideRepo.SaveAsync();
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;   
        //        return _response;
        //    }
        //    catch (Exception ex)
        //    { 
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //        = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[AllowAnonymous]
        //[HttpPost("DeleteGuide")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> DeleteGuide([FromHeader] int Id)
        //{
        //    try
        //    {
        //        if (await _guideRepo.GetAsync(a => a.Id == Id) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Hotels Not Found");
        //            return BadRequest(ModelState);
        //        }
        //        _guideRepo.DeleteAsync(Id);
        //       await _guideRepo.SaveAsync();
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;
        //        return _response;
        //    }
        //    catch (Exception ex)
        //    {


        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //            = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}
    }
}
