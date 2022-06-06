using DesafioAutoglass.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, Key> : IRepositoryBase<TEntity, Key> where TEntity : class
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            return (await DbSet.AddAsync(obj)).Entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(Key id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll(bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return DbSet.IgnoreQueryFilters().AsQueryable();
            }

            return DbSet.AsQueryable();
        }

        //public virtual IQueryable<TEntity> GetAllSoftDeleted()
        //{
        //    return DbSet.IgnoreQueryFilters()
        //        .Where(e => EF.Property<DateTime?>(e, "DeletionTime") != null);
        //}

        public virtual TEntity Update(TEntity obj)
        {
            return DbSet.Update(obj).Entity;
        }

        public virtual void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
