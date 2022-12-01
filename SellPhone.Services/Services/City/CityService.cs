using Microsoft.EntityFrameworkCore;
using SellPhone.Db.Data; 
using SellPhone.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.City
{
    public class CityService :  BaseService<LU_Cities, int>, ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        public CityService(IUnitOfWork unitOfWork, ICityService cityService) : base(unitOfWork.LU_CityRepository)
        {
            _unitOfWork = unitOfWork;
            _cityService = cityService;
        }

        public Task<IEnumerable<LU_Cities>> GetCities()
        {
            var cities = _unitOfWork.LU_CityRepository.GetAll();
            return cities;
        }
    }
}
