using AutoMapper;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using SellPhone.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.UserProfile
{
    public class UserProfileService : BaseService<UserProfiles, Guid>, IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public UserProfileService(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork.UserProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<UserProfiles> CreateUserProfile(UserProfileRequestDto requestDto, string userId)
        {
            UserProfiles profile = new  ();
            Guid Id = new Guid(userId);
            var u = _userService.GetUserById(Id);
            profile.AppUser = u;
            profile.Address = requestDto.Address;
            profile.FullName = requestDto.FullName;
            var Country = await _unitOfWork.LU_CountryRepository.Get(requestDto.CountryId);
            profile.Country = Country;
            var City = await _unitOfWork.LU_CityRepository.Get(requestDto.CityId);
            profile.City = City;
            profile.ProfilePicture = requestDto.ProfilePicture;
            profile.PhoneNumber = requestDto.PhoneNumber;
            profile.CreatedBy = Id;
            profile.CreatedAt = DateTime.Now;

            await _unitOfWork.UserProfileRepository.Add(profile);
            await _unitOfWork.SaveAsync();
            return profile;
        }
        
        public async Task<UserProfiles> UpdateUserProfile(UserProfileUpdateDto updateDto, string userId)
        {
            UserProfiles profile = new UserProfiles();
            Guid Id = new Guid(userId);
            var u = _userService.GetUserById(Id);
            profile.AppUser = u;
            profile.Address = updateDto.Address;
            profile.FullName = updateDto.FullName;
            var Country = await _unitOfWork.LU_CountryRepository.Get(updateDto.CountryId);
            profile.Country = Country;
            var City = await _unitOfWork.LU_CityRepository.Get(updateDto.CityId);
            profile.City = City;
            profile.ProfilePicture = updateDto.ProfilePicture;
            profile.PhoneNumber = updateDto.PhoneNumber;
            profile.UpdatedBy = Id;
            profile.UpdatedAt = DateTime.Now; 

            await _unitOfWork.UserProfileRepository.Update(profile);
            await _unitOfWork.SaveAsync();
            return profile;
        }

        public Task<UserProfiles> GetUserProfileByAspNetUserId(Guid AspNetUserId)
        {
            var profile = _unitOfWork.UserProfileRepository.GetUserProfileByAspNetUserId(AspNetUserId);
            return profile;
        }

        public bool IsUserProfileExist(Guid AspNetUserId)
        {
            var profile = GetUserProfileByAspNetUserId(AspNetUserId);
            if (profile != null && profile.Result != null)
                return true;
            else
                return false;
        }
    }
}
