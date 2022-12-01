using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SellPhone.Db.Data;
using SellPhone.Models.Consts;
using SellPhone.Models.DbModels;
using SellPhone.Models.Enums;
using SellPhone.Models.ViewModels;
using SellPhone.Services.Services.RedisCache;
using SellPhone.Services.Services.UserProfile;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        //private readonly IUserProfileService _userProfileService;
        public UserService(IUnitOfWork unitOfWork,
            IRedisCacheService redisCacheService,
            IConfiguration configuration, IMapper mapper,
            IPasswordHasher<AppUser> passwordHasher,
            UserManager<AppUser> userManager/*,IUserProfileService userProfileService*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            ReadConfiguration();
            //_userProfileService = userProfileService;
        }

        public async Task<UserResponseWithTokenModel> LoginUser(UserLoginModel loginVM)
        {
            ChaeckValidEmail(loginVM.Email);
            var userExists = await _userManager.FindByEmailAsync(loginVM.Email);
            if (userExists == null)
            {
                throw new ArgumentException($"No user found");
            }

            UserResponseWithTokenModel response = null;
            var signIn = await _userManager.CheckPasswordAsync(userExists, loginVM.Password);
            if (signIn == false)
            {
                throw new ArgumentException($"Invalid email or Password");
            }
            else
            {
                response = _mapper.Map<UserResponseWithTokenModel>(userExists);
                response.AccessToken = generateJwtToken(userExists.Id);
                response.RefresToken = generateRefreshToken(userExists.Id).Token; 
            }

            return response;
        }

        public async Task<MobileUserCreateResponseModel> LoginMobileUser(MobileUserLoginModel loginVM)
        {
            var userExists = _userManager.Users
                  .FirstOrDefault(x =>
                     x.PhoneNumber == loginVM.PhoneNumber
                     && x.CountryCode == loginVM.CountryCode);
            if (userExists == null)
            {
                throw new ArgumentException($"The phone number or country code is invalid!");
            }

            int otp = GenerateOTP();
            userExists.OTP = otp;
            await _userManager.UpdateAsync(userExists);
            return _mapper.Map<MobileUserCreateResponseModel>(userExists);
        }

        public async Task<UserResponseWithTokenModel> VerifyOTP(VerifyOTPModel verifyOtpVM)
        {
            var userExists = _userManager.Users
                  .FirstOrDefault(x =>
                     x.Id == verifyOtpVM.Id
                     && x.OTP == verifyOtpVM.OTP);
            if (userExists == null)
            {
                throw new ArgumentException($"Invalid OTP!");
            }

            UserResponseWithTokenModel response = null;

            response = _mapper.Map<UserResponseWithTokenModel>(userExists);
            response.AccessToken = generateJwtToken(userExists.Id);
            response.RefresToken = generateRefreshToken(userExists.Id).Token;

            return response;
        }

        public async Task<UserCreateResponseModel> ResetPassword(UserPasswordResetModel passwordResetVM)
        {
            if (passwordResetVM.Password != passwordResetVM.ConfirmPassword)
            {
                throw new ArgumentException($"Password and confirm password don't match");
            }

            var user = await _userManager.FindByIdAsync(passwordResetVM.Id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"No user found");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, passwordResetVM.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.First().Description);
            }

            var response = _mapper.Map<UserCreateResponseModel>(user);
            return response;
        }


        public UserRefreshTokenResponseModel CreateAccessTokenWithRefreshToken(UserRefreshTokenRquestModel refreshTokenRquestVM)
        {
            var value = _redisCacheService.GetRedisCacheValue((Guid)refreshTokenRquestVM.UserId, typeof(RefreshTokenModel).Name);

            UserRefreshTokenResponseModel result = null;
            if (!string.IsNullOrEmpty(value))
            {
                var refreshToken = JsonSerializer.Deserialize<RefreshTokenModel>(value);

                if (!refreshToken.IsExpired)
                {
                    if (refreshTokenRquestVM.RefresToken == refreshToken.Token)
                    {
                        // generate new jwt
                        var jwtToken = generateJwtToken((Guid)refreshTokenRquestVM.UserId);

                        result = new UserRefreshTokenResponseModel
                        {
                            UserId = refreshTokenRquestVM.UserId,
                            AccessToken = jwtToken,
                            RefresToken = refreshToken.Token
                        };
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid refresh token");
                    }
                }
                else
                {
                    _redisCacheService.DeleteRedisCacheValue((Guid)refreshTokenRquestVM.UserId, typeof(RefreshTokenModel).Name);
                    var jwtToken = generateJwtToken((Guid)refreshTokenRquestVM.UserId);
                    var refresToken = generateRefreshToken((Guid)refreshTokenRquestVM.UserId).Token;

                    result = new UserRefreshTokenResponseModel
                    {
                        UserId = refreshTokenRquestVM.UserId,
                        AccessToken = jwtToken,
                        RefresToken = refresToken
                    };

                }
            }

            if (result == null)
            {
                throw new ArgumentException($"No Refresh Token found");
            }

            return result;
        }

        public void RevokeToken(Guid userId, bool isLogout = false)
        {
            var value = _redisCacheService.GetRedisCacheValue(userId, typeof(RefreshTokenModel).Name);
            if (!string.IsNullOrEmpty(value))
            {
                _redisCacheService.DeleteRedisCacheValue(userId, typeof(RefreshTokenModel).Name);
            }
            else
            {
                if (isLogout)
                    throw new ArgumentException($"User is already logged out");
                else
                    throw new ArgumentException($"No token found to revoke against this user");

            }
        }

        private string generateJwtToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConstHelper.JWT_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(ConstHelper.ACCCESS_TOKEN_EXPIRY),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshTokenModel generateRefreshToken(Guid userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                var refreshToken = new RefreshTokenModel
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiredOn = DateTime.UtcNow.AddDays(ConstHelper.REFRESH_TOKEN_EXPIRY),
                    CreatedOn = DateTime.UtcNow,
                    UserId = userId,
                    Id = userId
                };

                _redisCacheService.SetRedishCacheValue(refreshToken);

                return refreshToken;
            }
        }

        public async Task<UserCreateResponseModel> AddUser(UserCreateRequestModel userCreateRequest)
        {
            var userType = UserRoles.User;// will change it when there are other roles
            if (userCreateRequest.Password != userCreateRequest.ConfirmPassword)
            {
                throw new ArgumentException($"Password and confirm password don't match");
            }
            ChaeckValidEmail(userCreateRequest.Email);
            var emailUser = await _userManager.FindByEmailAsync(userCreateRequest.Email);
            if (emailUser != null)
            {
                throw new ArgumentException($"This email is already registered");
            }
            
            var user = _mapper.Map<AppUser>(userCreateRequest);
            user.PasswordHash = _passwordHasher.HashPassword(user, user.Password);
            user.Password = "";
            user.UserName = userCreateRequest.Email;
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.First().Description);
            }
            else
            {
                string userRole = ((UserRoles)userType).ToString();
                await _userManager.AddToRoleAsync(user, userRole);
            }
            return _mapper.Map<UserCreateResponseModel>(user);
        }

        public async Task<MobileUserCreateResponseModel> AddMobileUser(MobileUserCreateRequestModel userCreateRequest)
        {
            var userType = UserRoles.User;// will change it when there are other roles
            MobileUserCreateResponseModel mobileUserCreateResponseModel = new MobileUserCreateResponseModel();
            var userExist = _userManager.Users
                  .FirstOrDefault(x =>
                     x.PhoneNumber == userCreateRequest.PhoneNumber
                     && x.CountryCode == userCreateRequest.CountryCode);
            if (userExist != null)
            {
                throw new ArgumentException($"This phone number is already registered");
            }

            var user = _mapper.Map<AppUser>(userCreateRequest);
            user.UserName = Guid.NewGuid().ToString();
            user.OTP = GenerateOTP();
            user.OTPCreatedAt = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.First().Description);
            }
            else
            {
                string userRole = ((UserRoles)userType).ToString();
                await _userManager.AddToRoleAsync(user, userRole);
            }
            return _mapper.Map<MobileUserCreateResponseModel>(user);
        }

        public async Task<string> ForgotPasswordToken(string Email)
        {
            ChaeckValidEmail(Email);
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                throw new ArgumentException($"No user found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;

        }

        public async Task<UserCreateResponseModel> Get(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"No user found");
            }
            return _mapper.Map<UserCreateResponseModel>(user);
        }

        public AppUser GetUserById(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(x=>x.Id == id);
            if (user == null)
            {
                throw new ArgumentException($"No user found, Invalid user ID.");
            }
            return user;
        }

        public async Task Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"No user found");
            }
            await _userManager.DeleteAsync(user);
        }

        public async Task<UserCreateResponseModel> Update(UserCreateResponseModel request)
        {
            ChaeckValidEmail(request.Email);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"No user found");
            }
            var exisitingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (exisitingEmail != null && exisitingEmail.Id != user.Id)
            {
                throw new ArgumentException($"This email already exist");
            }

            user.UserName = request.UserName;
            user.Email = request.Email;

            await _userManager.UpdateAsync(user);
            return _mapper.Map<UserCreateResponseModel>(user);
        }

        private void ChaeckValidEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                throw new ArgumentException($"Invalid email");
            }
        }

        private void ReadConfiguration()
        {
            ConstHelper.JWT_KEY = _configuration["JWT:Secret"];
            ConstHelper.ACCCESS_TOKEN_EXPIRY = int.Parse(_configuration["JWT:Access_Token_Expiry"]); ;
            ConstHelper.REFRESH_TOKEN_EXPIRY = int.Parse(_configuration["JWT:Refresh_Token_Expiry"]);
        }

        //Generate RandomNo
        public int GenerateOTP()
        {
            //int _min = 0000;
            //int _max = 9999;
            //Random _rdm = new Random();
            //return _rdm.Next(_min, _max);

            return 1000;
        }
    }
}
