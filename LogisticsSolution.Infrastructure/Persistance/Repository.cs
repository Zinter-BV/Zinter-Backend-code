using LogisticsSolution.Application.Contract.ExternalServices;
using LogisticsSolution.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LogisticsSolution.Infrastructure.Persistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an entity by integer ID asynchronously.
        /// </summary>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets an entity by string ID asynchronously.
        /// </summary>
        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Finds a list of entities based on a predicate with optional ordering.
        /// </summary>
        public async Task<List<T>> FindListAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = predicate != null ? _context.Set<T>().Where(predicate) : _context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        public async Task<List<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Finds a single entity with specified related entities included.
        /// </summary>
        public async Task<T?> FindSingleWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Finds a list of entities with specified related entities included.
        /// </summary>
        public async Task<List<T>> FindAllWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Checks if any entity exists based on a condition.
        /// </summary>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        /// <summary>
        /// Counts the number of entities that match a condition.
        /// </summary>
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        /// <summary>
        /// Adds a new entity to the context.
        /// </summary>
        public async Task<T> AddAsync(T entity)
        {
            var entry = await _context.Set<T>().AddAsync(entity);
            return entry.Entity;
        }

        /// <summary>
        /// Adds multiple entities to the context.
        /// </summary>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// Updates an existing entity in the context.
        /// </summary>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates multiple entities in the context.
        /// </summary>
        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        /// <summary>
        /// Deletes an entity from the context.
        /// </summary>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Deletes multiple entities from the context.
        /// </summary>
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Retrieves a paginated list of entities based on a predicate.
        /// </summary>
        public async Task<List<T>> PagedListAsync(
            Expression<Func<T, bool>> predicate,
            int pageSize,
            int pageNumber,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();


            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }



        /// <summary>
        /// Retrieves a paginated list of entities with specified related entities included.
        /// </summary>
        public async Task<List<T>> PagedListWithRelatedEntitiesAsync(
            Expression<Func<T, bool>> predicate,
            int pageSize,
            int pageNumber,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        /// <summary>
        /// Retrieve result as queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IQueryable<T> FindQueryable(Expression<Func<T, bool>> predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _context.Set<T>().Where(predicate);
            return orderBy != null ? orderBy(query) : query;
        }
        /// <summary>
        /// Retrieve result as queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> FindQueryableWithRelatedEntities(Expression<Func<T, bool>> predicate,
                                                           params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        /// <summary>
        /// gets all  with specified related entities included.
        /// </summary>
        public async Task<List<T>> GetAllWithRelatedEntitiesAsync(
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of entities based on a query.
        /// </summary>
        public async Task<List<T>> GetQueriableToPagedListAsync(
           IQueryable<T> query,
            int pageSize,
            int pageNumber,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }


    }
}
