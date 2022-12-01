using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SellPhone.Models.DbModels;
using System;

namespace SellPhone.Db.Data
{
    public class SellPhoneContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public SellPhoneContext(DbContextOptions<SellPhoneContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppPermission> AppPermissions { get; set; }
        public DbSet<AppRolePermission> AppRolePermissions { get; set; } 
        public DbSet<LU_Cities> LU_Cities { get; set; }
        public DbSet<LU_Brands> LU_Brands { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }
        public DbSet<LU_Countries> LU_Countries { get; set; }
        public DbSet<LU_Models> LU_Models { get; set; }
        public DbSet<AdPostings> AdPostings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
