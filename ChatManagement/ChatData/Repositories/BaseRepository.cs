using ChatData;
using ChatManagementData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatManagementData.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected ChatManagementContext _context;

    public BaseRepository(ChatManagementContext context) => _context = context;

    public void Add(T entity) => _context.Add(entity);

    public IQueryable<T> Query(Expression<Func<T, bool>> whereFilter = null)
    {
        DbSet<T> query = _context.Set<T>();
        return whereFilter != null ? query.Where(whereFilter) : query;
    }

    public void Remove(T entity) => _context.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => _context.RemoveRange(entities);

    public void SaveChanges() => _context.SaveChanges();
}
