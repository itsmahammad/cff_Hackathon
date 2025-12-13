using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CffHackathon.Infrastructure.Data.Configurations
{
    public abstract class BaseEntityMapping<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
