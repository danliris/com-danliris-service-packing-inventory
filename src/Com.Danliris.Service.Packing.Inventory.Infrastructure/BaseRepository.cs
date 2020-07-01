using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : StandardEntity
    {
        private const string UserAgent = "packing-sku-inventory-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public BaseRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public void Delete(TEntity entityToDelete)
        {
            EntityExtension.FlagForDelete(entityToDelete, _identityProvider.Username, UserAgent);
            _dbContext.Update(entityToDelete);
        }

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(entityToDelete, _identityProvider.Username, UserAgent);
            _dbContext.Update(entityToDelete);
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int page = 1, int size = 25)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).Skip((page -1) * size).Take(size).ToList();
            }
            else
            {
                return query.Skip((page - 1) * size).Take(size).ToList();
            }
        }

        public TEntity GetByID(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            EntityExtension.FlagForCreate(entity, _identityProvider.Username, UserAgent);
            _dbSet.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            EntityExtension.FlagForUpdate(entityToUpdate, _identityProvider.Username, UserAgent);
            _dbSet.Update(entityToUpdate);
        }
    }
}
