using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellPhone.Models.Dtos.UserProfile;
using System;
using SellPhone.Models.DbModels;
using SellPhone.Services.Services.UserProfile;
using SellPhone.Models.ViewModels.ResponseModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SellPhone.Models.Consts;
using Org.BouncyCastle.Asn1.Ocsp;

namespace SellPhone.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[AllowAnonymous]
    public class UserProfileController : BaseApiController<UserProfiles, UserProfileRequestDto, UserProfileResponseDto, UserProfileUpdateDto, Guid>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        private const string _entityName = ConstHelper.USER_PROFILE_ENTITY_NAME;
        public UserProfileController(IUserProfileService userProfileService, IMapper mapper) : base(userProfileService, mapper, _entityName)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateUserProfile")]
        public async Task<Response<UserProfileResponseDto>> CreateUserProfile([FromBody] UserProfileRequestDto requestDto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var resp = await _userProfileService.CreateUserProfile(requestDto, userId);
            string successMessage = _entityName + ConstHelper.CREATE_SUCCESS_MESSAGE;
            return CreateSuccessResponse<UserProfileResponseDto>(_mapper.Map<UserProfileResponseDto>(resp), successMessage);
        }

        [HttpPost]
        [Route("UpdateUserProfile")]
        public async Task<Response<UserProfileResponseDto>> UpdateUserProfile([FromBody] UserProfileUpdateDto updateDto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var resp = await _userProfileService.UpdateUserProfile(updateDto, userId);
            string successMessage = _entityName + ConstHelper.UPDATE_SUCCESS_MESSAGE;
            return CreateSuccessResponse<UserProfileResponseDto>(_mapper.Map<UserProfileResponseDto>(resp), successMessage);
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<Response<UserProfileResponseDto>> GetUserProfile()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            Guid Id = new Guid(userId);
            var resp = await _userProfileService.GetUserProfileByAspNetUserId(Id);
            string successMessage = _entityName + ConstHelper.GET_SUCCESS_MESSAGE;
            return CreateSuccessResponse<UserProfileResponseDto>(_mapper.Map<UserProfileResponseDto>(resp), successMessage);
        }
    }
}
