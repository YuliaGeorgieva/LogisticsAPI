using LogisticsApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LogisticsApi.Services.Base
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity, bool isSaveChanges = true)
        {
            await _context.Set<T>().AddAsync(entity);
            if (isSaveChanges)
                await _context.SaveChangesAsync();

            return entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();//.ToListAsync();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return _context.Set<T>().Where(filter);
        }

        public virtual async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity, bool isSaveChanges = true)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            if (isSaveChanges)
                await SaveChanges();

            return entity;
        }

        public async Task<bool> Delete(int id, bool isSaveChanges = true)
        {
            //_context.Entry(entity).State = EntityState.Deleted;
            var entity = await GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                if (isSaveChanges)
                    await SaveChanges();
                return true;
            }
            return false;
        }

        //Ranges functions
        public virtual async Task RemoveRange(IEnumerable<T> entities, bool isSaveChanges = true)
        {
            _context.Set<T>().RemoveRange(entities);
            if (isSaveChanges)
                await SaveChanges();
        }

        public virtual async Task AddRange(IList<T> entities, bool isSaveChanges = true)
        {
            _context.Set<T>().AddRange(entities);
            if (isSaveChanges)
                await SaveChanges();
        }

        public virtual async Task UpdateRange(IList<T> entities, bool isSaveChanges = true)
        {
            _context.Set<T>().UpdateRange(entities);
            if (isSaveChanges)
                await SaveChanges();
        }
    }
}
