using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.UserProfile
{
    public interface IUserProfileService : IBaseService<UserProfiles, Guid>
    {
        Task<UserProfiles> CreateUserProfile(UserProfileRequestDto requestDto,string userId);
        Task<UserProfiles> UpdateUserProfile(UserProfileUpdateDto updateDto, string userId);
        Task<UserProfiles> GetUserProfileByAspNetUserId(Guid AspNetUserId);
        bool IsUserProfileExist(Guid AspNetUserId);
    }
}
