using AgroBarn.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Configurations
{
    public class RefreshTokenConfiguration
    {
        public RefreshTokenConfiguration(EntityTypeBuilder<RefreshToken> entity)
        {
            entity.ToTable("RefreshToken");

            entity.HasKey(e => e.Token);

            entity.Property(e => e.JwtId)
                .IsRequired()
                .HasColumnType("nvarchar(4000)");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationUser)
                .WithMany(p => p.RefreshToken)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_AspNetUsers");
        }
    }
}
