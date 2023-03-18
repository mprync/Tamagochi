using System.Linq.Expressions;
using Tamagotchi.Data.Models.Interfaces;

namespace Tamagotchi.Data.Repositories.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<IQueryable<T>> GetManyQueryable(Expression<Func<T, bool>> predicate);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int id);
}