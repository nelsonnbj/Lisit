using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using SystemQuickzal.Data.Models.Configuration;
using SystemTheLastBugSpa.Data.Entity;


namespace SystemTheLastBugSpa.Data
{
    public class AplicationDataContext : IdentityDbContext<Users, Roles, Guid>
    {
        public AplicationDataContext(DbContextOptions<AplicationDataContext> options) : base(options)
        {

        }
        
       
        public virtual DbSet<RolPermissions> RolPermissions { get; set; }
       
        public virtual DbSet<Comuna> Comuna { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SocialHelp> SocialHelp { get; set; }
        public virtual DbSet<ServicesByPeople> ServicesByPeople { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityModelConfiguration.ContinueModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
