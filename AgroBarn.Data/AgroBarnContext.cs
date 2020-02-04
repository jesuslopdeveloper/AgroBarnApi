using AgroBarn.Domain.Entities;
using AgroBarn.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AgroBarn.Data
{
    public partial class AgroBarnContext : DbContext
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

        //Catalogs
        public virtual DbSet<BreedDto> BreedDto { get; set; }
        public virtual DbSet<ConceptionDto> ConceptionDto { get; set; }
        public virtual DbSet<MessageDto> MessageDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables configuration
            //Catalogs
            new BreedConfiguration(modelBuilder.Entity<BreedDto>());
            new ConceptionConfiguration(modelBuilder.Entity<ConceptionDto>());
            new MessageConfiguration(modelBuilder.Entity<MessageDto>());
        }
    }
}