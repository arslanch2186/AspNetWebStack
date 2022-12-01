using Microsoft.AspNetCore.Http;
using SellPhone.Models.Consts;
using SellPhone.Models.DbModels;
using SellPhone.Models.ViewModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace SellPhone.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException exc)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var response = CreateErrorResponse<AppUser>(exc.Message, context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                var response = CreateErrorResponse<AppUser>("Request is forbidden", context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (ArgumentException exc)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = CreateErrorResponse<AppUser>(exc.Message, context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (ValidationException exc)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = CreateErrorResponse<AppUser>(exc.Message, context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception exc)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = CreateErrorResponse<AppUser>(exc.Message, context.Response.StatusCode);
                //var response = CreateErrorResponse<AppUser>(ConstHelper.ERROR_MESSAGE, context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        protected Response<TResponse> CreateErrorResponse<TResponse>(string message, int error)
        {
            return new Response<TResponse>
            {
                Status = ConstHelper.RESPONSE_ERROR,
                Message = message
            };
            //return new Response
            //{
            //    Status = ConstHelper.RESPONSE_ERROR,
            //    Error = new Error
            //    {
            //        Code = error.ToString(),
            //        Message = message
            //    }
            //};
        }
    }
}