namespace LogisticsSolution.Application.Contract.ExternalServices
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose(bool disposing);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        int SaveChanges();

    }
}
