using AgroBarn.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Configurations
{
    public class PeopleConfiguration
    {
        public PeopleConfiguration(EntityTypeBuilder<PeopleDto> entity)
        {
            entity.ToTable("Peoples");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.FirstSurname)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.SecondSurname)
                .HasMaxLength(100);

            entity.Property(e => e.DateCreate).HasColumnType("datetime");

            entity.Property(e => e.DateModify).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationUser)
                .WithMany(p => p.PeopleDto)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Peoples_AspNetUsers");
        }
    }
}
