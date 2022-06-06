using System;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, Key> : IDisposable where TEntity: class
    {
        Task<TEntity> AddAsync(TEntity obj);
        TEntity Update(TEntity obj);
        void Remove(TEntity obj);
        Task<TEntity> GetByIdAsync(Key id);
        IQueryable<TEntity> GetAll(bool includeDeleted);
        Task SaveChangesAsync();
    }
}
