using AgroBarn.Domain.Entities;
using AgroBarn.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AgroBarn.Data
{
    public partial class AgroBarnContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AgroBarnContext()
        {
        }

        public AgroBarnContext(DbContextOptions<AgroBarnContext> options)
            : base(options)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------
        //Tables
        //Identity
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<PeopleDto> PeopleDto { get; set; }

        //Catalogs
        public virtual DbSet<BreedDto> BreedDto { get; set; }
        public virtual DbSet<ConceptionDto> ConceptionDto { get; set; }
        public virtual DbSet<MessageDto> MessageDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Enable migration Identity tables
            base.OnModelCreating(modelBuilder);

            //Identity
            new RefreshTokenConfiguration(modelBuilder.Entity<RefreshToken>());
            new ApplicationRoleConfiguration(modelBuilder.Entity<ApplicationRole>());
            new PeopleConfiguration(modelBuilder.Entity<PeopleDto>());

            //Catalogs
            new BreedConfiguration(modelBuilder.Entity<BreedDto>());
            new ConceptionConfiguration(modelBuilder.Entity<ConceptionDto>());
            new MessageConfiguration(modelBuilder.Entity<MessageDto>());
        }
    }
}