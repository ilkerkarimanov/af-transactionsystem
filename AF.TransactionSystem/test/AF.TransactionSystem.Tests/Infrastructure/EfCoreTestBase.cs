using AF.TransactionSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AF.TransactionSystem.Tests.Infrastructure
{
    public abstract class EfCoreTestBase : IDisposable
    {
        protected readonly TransactionSystemDbContext _dbContext;

        protected EfCoreTestBase()
        {
            var _contextOptions = new DbContextOptionsBuilder<TransactionSystemDbContext>()
                .UseInMemoryDatabase($"TransactionSystemDbContext{Guid.NewGuid()}").Options;

            _dbContext = new TransactionSystemDbContext(_contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
