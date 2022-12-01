using AutoMapper;
using SellPhone.Models.DbModels;
using SellPhone.Models.ViewModels.ResponseModels;
using SellPhone.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SellPhone.Models.Consts;

namespace SellPhone.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseApiController<T, TRequest, TResponse, TUpdate, Tkey> : ApiBaseController where T : SoftDeletedEntity<Tkey>, new()
        where TRequest : class where TResponse : class
    {
        private readonly IBaseService<T, Tkey> _baseService;
        private IMapper _mapper { get; set; }
        private string _entityName { get; set; }
        protected BaseApiController(IBaseService<T, Tkey> service, IMapper mapper, string entityName)
        {
            _baseService = service;
            _mapper = mapper;
            _entityName = entityName;
        }

        /// <summary>
        /// Get the Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}")]
        public virtual async Task<Response<TResponse>> Get(Tkey id)
        {
            Response<TResponse> response = null;

            var result = await _baseService.Get(id);

            if (result != null)
            {
                var responseData = _mapper.Map<TResponse>(result);
                string successMessage = _entityName + ConstHelper.GET_SUCCESS_MESSAGE;
                response = CreateSuccessResponse(responseData, successMessage);
            }
            else
            {
                throw new ValidationException("No data found against this request");
            }

            return response;
        }

        /// <summary>
        /// Create the Entity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public virtual async Task<Response<TResponse>> Create([FromBody] TRequest request)
        {
            T obj = _mapper.Map<T>(request);
            await _baseService.Add(obj);
            var responseData = _mapper.Map<TResponse>(obj);
            string successMessage = _entityName + ConstHelper.CREATE_SUCCESS_MESSAGE;
            return CreateSuccessResponse(responseData, successMessage);
        }

        /// <summary>
        /// Delete the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public virtual async Task<Response<TResponse>> Delete(Tkey id)
        {
            await _baseService.Delete(id);
            string successMessage = _entityName + ConstHelper.DELETE_SUCCESS_MESSAGE;
            return CreateSuccessResponse<TResponse>(successMessage);
        }

        /// <summary>
        /// Update the Entity
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public virtual async Task<Response<TResponse>> Update(Tkey id, [FromBody] TUpdate update)
        {
            T obj = _mapper.Map<T>(update);
            await _baseService.Update(obj);
            var responseData = _mapper.Map<TResponse>(obj);
            var successMessage = _entityName + ConstHelper.UPDATE_SUCCESS_MESSAGE;
            return CreateSuccessResponse(responseData,successMessage);
        }



    }
}
