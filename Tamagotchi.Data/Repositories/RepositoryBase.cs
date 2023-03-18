using Microsoft.EntityFrameworkCore;
using Tamagotchi.Data.Models.Interfaces;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Data.Repositories;

public class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    private readonly TContext context;

    public RepositoryBase(TContext context)
    {
        this.context = context;
    }

    public virtual async Task<TEntity> Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> Delete(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        
        if (entity == null)
        {
            return null;
        }

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();

        return entity;
    }
    
    public virtual DbSet<TEntity> GetManyQueryable()
    {
        return context.Set<TEntity>();
    }

    public virtual async Task<TEntity> Get(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entity;
    }
}