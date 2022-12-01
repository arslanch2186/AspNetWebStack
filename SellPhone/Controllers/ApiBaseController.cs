using Microsoft.AspNetCore.Mvc;
using SellPhone.Models.Consts;
using SellPhone.Models.ViewModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SellPhone.Controllers
{
    public class ApiBaseController : ControllerBase
    {

        protected Guid GetUserId()
        {
            return Guid.Parse(this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value);
        }

        protected Response<TResponse> CreateSuccessResponse<TResponse>(string message)
        {
            return new Response<TResponse>
            {
                Status = ConstHelper.RESPONSE_SUCCESS,
                Message = message
            };
        }

        protected Response<TResponse> CreateSuccessResponse<TResponse>(TResponse responseData, string message)
        {
            return new Response<TResponse>
            {
                Status = ConstHelper.RESPONSE_SUCCESS,
                Message = message,
                Data = responseData
            };
        }
    }
}
