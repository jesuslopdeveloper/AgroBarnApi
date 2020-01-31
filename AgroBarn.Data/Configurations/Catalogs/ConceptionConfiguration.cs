using AgroBarn.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Configurations
{
    public class ConceptionConfiguration
    {
        public ConceptionConfiguration(EntityTypeBuilder<ConceptionDto> entity)
        {
            entity.ToTable("Conceptions");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime");

            entity.Property(e => e.DateModify)
                .HasColumnType("datetime");
        }
    }
}
