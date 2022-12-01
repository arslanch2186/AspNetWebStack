using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using SellPhone.Services.Services.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.AdPosting
{
    public class AdPostingService : BaseService<AdPostings, Guid>, IAdPostingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdPostingService _adPostingService;
        public AdPostingService(IUnitOfWork unitOfWork, IAdPostingService adPostingService) : base(unitOfWork.AdPostingRepository)
        {
            _unitOfWork = unitOfWork;
            _adPostingService = adPostingService;
        }
    }
}
