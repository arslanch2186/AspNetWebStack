using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellPhone.Db.Data;
using SellPhone.Models.Consts;
using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using SellPhone.Models.ViewModels.ResponseModels;
using SellPhone.Services.Services.City;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SellPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public LookupsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetCities")]
        public async Task<Response<IEnumerable<CityResponseDto>>> GetCities(int countryId)
        {
            var resp = await _unitOfWork.LU_CityRepository.GetAll();
            resp = resp.Where(x => x.CountryId == countryId).ToList(); // to do
            string successMessage = ConstHelper.CITY_ENTITY_NAME + ConstHelper.GET_SUCCESS_MESSAGE;
            return CreateSuccessResponse<IEnumerable<CityResponseDto>>(_mapper.Map<IEnumerable<CityResponseDto>>(resp), successMessage);

        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetCountries")]
        public async Task<Response<IEnumerable<CoutryResponseDto>>> GetCountries()
        {
            var resp = await _unitOfWork.LU_CountryRepository.GetAll();
            string successMessage = ConstHelper.COUNTRY_ENTITY_NAME + ConstHelper.GET_SUCCESS_MESSAGE;
            return CreateSuccessResponse<IEnumerable<CoutryResponseDto>>(_mapper.Map<IEnumerable<CoutryResponseDto>>(resp), successMessage);

        }
    }
}









//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SellPhone.Models.Dtos.UserProfile;
//using SellPhone.Models.DbModels;
//using SellPhone.Models.Consts;
//using SellPhone.Services.Services.City;
//using SellPhone.Models.ViewModels.ResponseModels;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using SellPhone.Db.Data;

//namespace SellPhone.Controllers
//{
//    [ApiController]
//    [ApiVersion("1.0")]
//    [Route("api/[controller]")]

//    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    [AllowAnonymous]
//    public class CitiesController : BaseApiController<LU_Cities, CityRequestDto, CityResponseDto, CityUpdateDto, int>
//    {
//        private readonly ICityService _cityService;
//        private readonly IMapper _mapper;
//        private readonly IUnitOfWork _unitOfWork;
//        private const string _entityName = ConstHelper.CITY_ENTITY_NAME;
//        public CitiesController(/*ICityService cityservice,*/ IMapper mapper, IUnitOfWork unitOfWork) : base(cityservice, mapper, _entityName)
//        {
//            //_cityService = cityservice;
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//        }

//        /// <summary>
//        /// Get all cities
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("GetCities")]
//        public async Task<Response<IEnumerable<CityResponseDto>>> GetCities()
//        {
//            var resp = await _unitOfWork.LU_CityRepository.GetAll();
//            string successMessage = _entityName + ConstHelper.GET_SUCCESS_MESSAGE;
//            return CreateSuccessResponse<IEnumerable<CityResponseDto>>(_mapper.Map<IEnumerable<CityResponseDto>>(resp), successMessage);
//        }
//    }
//}



