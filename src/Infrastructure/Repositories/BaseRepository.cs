using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T>(DataContext context) : IBaseRepository<T> where T : class
{
    protected readonly DataContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public IQueryable<T> GetAll() => DbSet.AsNoTracking();
    public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id); 

    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);

    public Task<int> SaveChangesAsync() =>  Context.SaveChangesAsync();
}