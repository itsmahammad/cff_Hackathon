using System.Reflection;

namespace CffHackathon.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
