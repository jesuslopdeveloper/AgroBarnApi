using AgroBarn.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroBarn.Data.Configurations
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entity)
        {
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
