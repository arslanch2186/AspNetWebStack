using SellPhone.Db.Repositories.AdPosting;
using SellPhone.Db.Repositories.AppPermissions;
using SellPhone.Db.Repositories.AppRoles;
using SellPhone.Db.Repositories.IUserProfileRepository;
using SellPhone.Db.Repositories.Lookup.LU_City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IAppRoleRepository _roleRepository;
        private IAppPermissionRepository _permissionRepository;
        private IUserProfileRepository _userProfileRepository;
        private ILU_CountryRepository _countryRepository;
        private ILU_CityRepository _cityRepository;
        private IAdPostingRepository _adPostingRepository;
        private readonly SellPhoneContext _context;
        public UnitOfWork(SellPhoneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAppRoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ??= new AppRoleRepository(_context);
            }
        }

        public IAppPermissionRepository PermissionRepository
        {
            get
            {
                return _permissionRepository ??= new AppPermissionRepository(_context);
            }
        }   
        
        public IUserProfileRepository UserProfileRepository
        {
            get
            {
                return _userProfileRepository ??= new UserProfileRepository(_context);
            }
        }
        
        public ILU_CountryRepository LU_CountryRepository
        {
            get
            {
                return _countryRepository ??= new LU_CountryRepository(_context);
            }
        }
        
        public ILU_CityRepository LU_CityRepository
        {
            get
            {
                return _cityRepository ??= new LU_CityRepository(_context);
            }
        }
        
        public IAdPostingRepository AdPostingRepository
        {
            get
            {
                return _adPostingRepository ??= new AdPostingRepository(_context);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
