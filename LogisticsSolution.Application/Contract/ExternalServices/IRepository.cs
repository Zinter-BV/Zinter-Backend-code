using System.Linq.Expressions;

namespace LogisticsSolution.Application.Contract.ExternalServices
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets an entity by integer ID asynchronously.
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Gets an entity by string ID asynchronously.
        /// </summary>
        Task<T?> GetByIdAsync(string id);


        /// <summary>
        /// Gets an entity by long ID asynchronously.
        /// </summary>
        Task<T?> GetByIdAsync(long id);

        /// <summary>
        /// Finds a list of entities based on a predicate with optional ordering.
        /// </summary>
        Task<List<T>> FindListAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        Task<List<T>> FindAllAsync();

        /// <summary>
        /// Finds a single entity with specified related entities included.
        /// </summary>
        Task<T?> FindSingleWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Finds a list of entities with specified related entities included.
        /// </summary>
        Task<List<T>> FindAllWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Checks if any entity exists based on a condition.
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Counts the number of entities that match a condition.
        /// </summary>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the context.
        /// </summary>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Adds multiple entities to the context.
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates an existing entity in the context.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Updates multiple entities in the context.
        /// </summary>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes an entity from the context.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Deletes multiple entities from the context.
        /// </summary>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Retrieves a paginated list of entities based on a predicate.
        /// </summary>
        Task<List<T>> PagedListAsync(
            Expression<Func<T, bool>> predicate,
            int pageSize,
            int pageNumber,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
             params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Retrieves a paginated list of entities with specified related entities included.
        /// </summary>
        Task<List<T>> PagedListWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            int pageSize,
            int pageNumber,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Retrieves result as queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<T> FindQueryable(Expression<Func<T, bool>> predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Retrieves result as queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> FindQueryableWithRelatedEntities(Expression<Func<T, bool>> predicate,
                                                           params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// Retrieves result as list
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<T>> GetAllWithRelatedEntitiesAsync(
    params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Retrieves a paginated list of entities based on a query.
        /// </summary>
        Task<List<T>> GetQueriableToPagedListAsync(
             IQueryable<T> query,
              int pageSize,
              int pageNumber,
              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
              params Expression<Func<T, object>>[] includes);

    }
}
