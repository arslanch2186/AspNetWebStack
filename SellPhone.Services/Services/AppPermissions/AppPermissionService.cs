using AutoMapper;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.AppPermissions
{
    public class AppPermissionService : BaseService<AppPermission, int>, IAppPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppPermissionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.PermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    }
}
