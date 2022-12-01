using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using SellPhone.Models.Consts;
using SellPhone.Models.DbModels;
using SellPhone.Models.Helpers;
using SellPhone.Models.ViewModels;
using SellPhone.Models.ViewModels.ResponseModels;
using SellPhone.Services.Services.EmailService;
using SellPhone.Services.Services.UserProfile;
using SellPhone.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SellPhone.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserProfileService _userProfileService;

        public UserController(IUserService userService, IMapper mapper, IEmailSender emailSender, IUserProfileService userProfileService)
        {
            _userService = userService;
            _mapper = mapper;
            _emailSender = emailSender;
            _userProfileService = userProfileService;
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// get the user data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<Response<UserCreateResponseModel>> Get(Guid id)
        {
            var user = await _userService.Get(id);
            string successMessage = "User retrieved successfully";
            return CreateSuccessResponse(_mapper.Map<UserCreateResponseModel>(user), successMessage);
        }

        /// <summary>
        /// register the user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<Response<UserCreateResponseModel>> Create([FromBody] UserCreateRequestModel request)
        {
            var responseData = await _userService.AddUser(request);
            string successMessage = "User registered successfully";
            return CreateSuccessResponse(responseData, successMessage);
        }

        /// <summary>
        /// register user phone number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterMobileNumber")]
        [AllowAnonymous]
        public async Task<Response<MobileUserCreateResponseModel>> RegisterMobileNumber([FromBody] MobileUserCreateRequestModel request)
        {
            var responseData = await _userService.AddMobileUser(request);
            string successMessage = "Mobile number registered. Please verify OTP to proceed.";
            return CreateSuccessResponse(responseData, successMessage);
        }

        /// <summary>
        /// update the user name and email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<Response<UserCreateResponseModel>> Update([FromBody] UserCreateResponseModel request)
        {
            var responseData = await _userService.Update(request);
            string successMessage = "User updated successfully";
            return CreateSuccessResponse(responseData, successMessage);
        }


        /// <summary>
        /// user login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<Response<UserResponseWithTokenModel>> LoginUser([FromBody] UserLoginModel request)
        {
            var user = await _userService.LoginUser(request);
            if (user != null && user.Id != null)
            {
                user.isUserProfileSet = _userProfileService.IsUserProfileExist((Guid)user.Id);
            }
            user.Id = null;
            string successMessage = "Login successful";
            return CreateSuccessResponse(_mapper.Map<UserResponseWithTokenModel>(user), successMessage);
        }

        /// <summary>
        /// Mobile user login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginPhoneNumber")]
        [AllowAnonymous]
        public async Task<Response<MobileUserCreateResponseModel>> LoginPhoneNumber([FromBody] MobileUserLoginModel request)
        {
            var responseData = await _userService.LoginMobileUser(request);
            string successMessage = "Mobile number verified. Please verify OTP to proceed.";
            return CreateSuccessResponse(responseData, successMessage);

        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("verifyOTP")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<Response<UserResponseWithTokenModel>> verifyOTP([FromBody] VerifyOTPModel request)
        {
            //var claimsIdentity = this.User.Identity as ClaimsIdentity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            //Guid Id = new Guid(userId);
            //request.Id = Id;
            var response = await _userService.VerifyOTP(request);
            string successMessage = "OTP verified successfully";
            return CreateSuccessResponse(_mapper.Map<UserResponseWithTokenModel>(response),successMessage);
        }

        /// <summary>
        /// delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<Response<AppUser>> Delete(Guid id)
        {
            await _userService.Delete(id);
            string successMessage = "User deleted";
            return CreateSuccessResponse<AppUser>(successMessage);
        }

        /// <summary>
        /// reset user password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public async Task<Response<UserCreateResponseModel>> ResetPassword([FromBody] UserPasswordResetModel request)
        {
            var user = await _userService.ResetPassword(request);
            string successMessage = "Reset password successful";
            return CreateSuccessResponse(_mapper.Map<UserCreateResponseModel>(user),successMessage);
        }


        /// <summary>
        /// refresh access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RefreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Response<UserRefreshTokenResponseModel>> RefreshAccessToken([FromBody] UserRefreshTokenRquestModel request)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            Guid Id = new Guid(userId);
            request.UserId = Id;
            var user = _userService.CreateAccessTokenWithRefreshToken(request);
            string successMessage = "Token is refreshed";
            return CreateSuccessResponse(_mapper.Map<UserRefreshTokenResponseModel>(user),successMessage);
        }

        /// <summary>
        /// revok refresh token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RevokeToken")]
        [AllowAnonymous]
        public async Task<Response<AppUser>> RevokeToken(Guid userId)
        {
            return await RevokeTokenOrLogout(userId);
        }

        /// <summary>
        /// user logout
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogout")]
        public async Task<Response<AppUser>> UserLogout(Guid userId)
        {
            return await RevokeTokenOrLogout(userId, true);
        }

        private async Task<Response<AppUser>> RevokeTokenOrLogout(Guid userId, bool isLogout = false)
        {
            _userService.RevokeToken(userId, isLogout);
            string successMessage = "user is logged out";
            return CreateSuccessResponse<AppUser>(successMessage);
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<Object> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var token = await _userService.ForgotPasswordToken(forgotPasswordModel.Email);
            var link = this.Url.ActionLink("ResetPassword", "Account", new { token, email = forgotPasswordModel.Email }, Request.Scheme);

            var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            var message = new Message(new string[] { forgotPasswordModel.Email }, "Reset Password", "Please click the url to reset your password. \n" + link, emailConfig.SenderName);
            var emailResponse = _emailSender.SendEmail(message);

            if (emailResponse)
                return CreateSuccessResponse<TokenOptions>("A link is sent on your email to reset password.");
            else
                throw new ValidationException("Email could not be sent. Please try again in a while.");
        }
    }
}
