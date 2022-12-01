using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellPhone.Models.Consts;
using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using SellPhone.Services.Services.AdPosting;
using SellPhone.Services.Services.EmailService;
using SellPhone.Services.Services.UserProfile;
using SellPhone.Services.Services.Users;
using System;
using static SellPhone.Models.Dtos.AdPosting.AdPostingDto;

namespace SellPhone.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdPostingController : BaseApiController<AdPostings, AdPostingRequestDto, AdPostingResponseDto, AdPostingUpdateDto, Guid>
    {
        private readonly IAdPostingService _adPostingService;
        private readonly IMapper _mapper;
        private const string _entityName = ConstHelper.AD_POSTING_ENTITY_NAME;
        public AdPostingController(IAdPostingService adPostingService, IMapper mapper) : base(adPostingService, mapper, _entityName)
        {
            _adPostingService = adPostingService;
            _mapper = mapper;
        }



    }
}
