using Microsoft.EntityFrameworkCore;
using Tamagotchi.Data.Models.Interfaces;

namespace Tamagotchi.Data.Repositories.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    DbSet<T> GetManyQueryable();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int id);
}