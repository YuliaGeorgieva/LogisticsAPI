using System.Linq.Expressions;

namespace LogisticsApi.Services.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity, bool isSaveChanges = true);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> GetById(Guid id);
        Task<T> GetById(int id);

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);


        Task<T> Update(T entity, bool isSaveChanges = true);
        Task<bool> Delete(int id, bool isSaveChanges = true);
        Task RemoveRange(IEnumerable<T> entities, bool isSaveChanges = true);
        Task AddRange(IList<T> entities, bool isSaveChanges = true);
        Task UpdateRange(IList<T> entities, bool isSaveChanges = true);

        Task SaveChanges();
    }
}
