using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Common;

namespace UltraGroupHotelAPI.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByTypeAsync(Expression<Func<T, bool>> expr);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expr);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expr=null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expr = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T,object>>> includes = null,
                                        bool disableTracking = true);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);

        void AddEntity(T entity);
        void RemoveEntity(T entity);
        void UpdateEntity(T entity);
    }
}
