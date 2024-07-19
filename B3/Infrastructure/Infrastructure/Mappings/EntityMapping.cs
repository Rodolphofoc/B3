using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : Entity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(entity => entity.IntegrationId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NewId()");

        }
    }
}
