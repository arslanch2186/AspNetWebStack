using SellPhone.Models.DbModels;
using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.Users
{
    public interface IUserService
    {

        Task<UserResponseWithTokenModel> LoginUser(UserLoginModel loginVM);
        Task<UserCreateResponseModel> ResetPassword(UserPasswordResetModel request);
        void RevokeToken(Guid userId, bool isLogout = false);
        UserRefreshTokenResponseModel CreateAccessTokenWithRefreshToken(UserRefreshTokenRquestModel request);
        Task<UserCreateResponseModel> AddUser(UserCreateRequestModel user);
        Task<MobileUserCreateResponseModel> AddMobileUser(MobileUserCreateRequestModel user);
        Task Delete(Guid id);
        Task<UserCreateResponseModel> Update(UserCreateResponseModel request);
        Task<UserCreateResponseModel> Get(Guid id);
        Task<MobileUserCreateResponseModel> LoginMobileUser(MobileUserLoginModel loginVM);
        Task<UserResponseWithTokenModel> VerifyOTP(VerifyOTPModel verifyOtpVM);
        Task<string> ForgotPasswordToken(string Email);
        AppUser GetUserById(Guid id);
    }
}
