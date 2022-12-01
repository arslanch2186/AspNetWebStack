using AutoMapper;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.AppRoles
{
    public class AppRoleService : BaseService<AppRole, int>, IAppRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppRoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.RoleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    }
}
