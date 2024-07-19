using Domain.Domain;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class B3Context : DbContext
    {
        public B3Context(DbContextOptions<B3Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(B3Context).Assembly);

            var entityTypes = modelBuilder.Model
                                                        .GetEntityTypes()
                                                        .Where(t => typeof(Entity).IsAssignableFrom(t.ClrType));

            foreach (var entityType in entityTypes)
            {
                var configurationType = typeof(EntityMapping<>)
                    .MakeGenericType(entityType.ClrType);

                modelBuilder
                    .ApplyConfiguration((dynamic)Activator.CreateInstance(configurationType));
            }


        }
        public DbSet<TaskManager> TaskManager { get; set; }
    }
}
