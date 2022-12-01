using AutoMapper;
using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.SellPhoneProfiles
{
    public class SellPhoneProfiles : Profile
    {
        public SellPhoneProfiles()
        {
            CreateMap<AppUser, UserResponseWithTokenModel>().ReverseMap();
            CreateMap<AppUser, UserCreateResponseModel>().ReverseMap();
            CreateMap<AppUser, UserCreateRequestModel>().ReverseMap();
            CreateMap<AppUser, UserCreateResponseModel>().ReverseMap();
            CreateMap<AppUser, MobileUserCreateRequestModel>().ReverseMap();
            CreateMap<AppUser, MobileUserCreateResponseModel>().ReverseMap();
            CreateMap<UserProfiles, UserProfileRequestDto>().ReverseMap();
            CreateMap<UserProfiles, UserProfileResponseDto>().ReverseMap(); 
            CreateMap<UserProfiles, UserProfileUpdateDto>().ReverseMap(); 
            CreateMap<LU_Cities, CityResponseDto>().ReverseMap(); 
            CreateMap<LU_Countries, CoutryResponseDto>().ReverseMap(); 
        }
    }
}
