using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.ViewModels
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length should be greater than or equal to 6")]
        public string Password { get; set; }
    }

    public class MobileUserLoginModel
    {
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }

    public class UserPasswordResetModel : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length should be greater than or equal to 6")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Confirm password length should be greater than or equal to 6")]
        public string ConfirmPassword { get; set; }
    }

    public class UserCreateRequestModel /*: UserModel*/
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password at least should be of length 6")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Confirm Password at least should be of length 6")]
        public string ConfirmPassword { get; set; }
    }

    public class MobileUserCreateRequestModel
    {
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }

    public class VerifyOTPModel
    {
        public Guid? Id { get; set; }
        [Required]
        public int OTP { get; set; }
    }

    public class UserCreateResponseModel : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class MobileUserCreateResponseModel : BaseEntity
    {
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserRefreshTokenRquestModel
    {
        public Guid? UserId { get; set; }
        [Required]
        public string RefresToken { get; set; }
    }


    public class UserRefreshTokenResponseModel : UserRefreshTokenRquestModel
    {
        public string AccessToken { get; set; }
    }


    public class UserUpdateRequestModel : UserModel
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class RefreshTokenModel : BaseEntity
    {
        public RefreshTokenModel()
        {
            Id = new Guid();
        }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiredOn;
        public DateTime CreatedOn { get; set; }
    }

    public class UserResponseWithTokenModel : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefresToken { get; set; }
        public bool? isUserProfileSet { get; set; }
        public Guid? Id { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
