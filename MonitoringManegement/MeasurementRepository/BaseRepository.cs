using MeasurementRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonitoringManagementData;
using System.Linq.Expressions;

namespace MeasurementRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected MonitoringManagementContext _context;
        private readonly IConfiguration _config;

        public BaseRepository(IConfiguration config)
        {
            _config = config;
            _context = new MonitoringManagementContext(_config);
        }

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
}