using System.Linq.Expressions;

namespace UserManagementData.Repositories
{
    public interface IBaseRepository<T>
    {
        public void Add(T entity);

        public IQueryable<T> Query(Expression<Func<T, bool>> whereFilter = null);

        public void Remove(T entity);

        public void RemoveRange(IEnumerable<T> entities);

        public void SaveChanges();
    }
}
