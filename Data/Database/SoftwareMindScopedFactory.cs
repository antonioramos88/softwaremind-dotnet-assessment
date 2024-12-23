using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class SoftwareMindScopedFactory : IDbContextFactory<SoftwareMindContext>
    {
        private readonly IDbContextFactory<SoftwareMindContext> PooledFactory;

        public SoftwareMindScopedFactory(
            IDbContextFactory<SoftwareMindContext> pooledFactory)
        {
            PooledFactory = pooledFactory;
        }

        public SoftwareMindContext CreateDbContext()
        {
            return PooledFactory.CreateDbContext();
        }
    }
}
