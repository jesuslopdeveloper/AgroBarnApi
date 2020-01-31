using AgroBarn.Domain.Entities;
using AgroBarn.Data.Configurations;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=AgroBarn;User=sa;Password=siavilla;Trusted_Connection=True;");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------
        //Tables

        //Catalogs
        public virtual DbSet<BreedDto> BreedDto { get; set; }
        public virtual DbSet<ConceptionDto> ConceptionDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables configuration
            //Catalogs
            new BreedConfiguration(modelBuilder.Entity<BreedDto>());
            new ConceptionConfiguration(modelBuilder.Entity<ConceptionDto>());
        }
    }
}