using AgroBarn.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Configurations
{
    public class MessageConfiguration
    {
        public MessageConfiguration(EntityTypeBuilder<MessageDto> entity)
        {
            entity.ToTable("Messages");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Module)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("nvarchar(300)");

            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime");

            entity.Property(e => e.DateModify)
                .HasColumnType("datetime");
        }
    }
}
