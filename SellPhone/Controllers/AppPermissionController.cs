using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellPhone.Models.DbModels;
using SellPhone.Models.ViewModels;
using SellPhone.Models.ViewModels.ResponseModels;
using SellPhone.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SellPhone.Services.Services.AppRoles;

namespace SellPhone.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppPermissionController : ApiBaseController
    {
        private readonly IAppRoleService _roleService;
        private readonly IMapper _mapper;
        public AppPermissionController(IAppRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

    }
}
