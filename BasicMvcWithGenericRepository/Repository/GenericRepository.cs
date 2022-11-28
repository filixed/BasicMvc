using System.Linq.Expressions;
using BasicMvcWithGenericRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicMvcWithGenericRepository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly PostgreDbContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(PostgreDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }
    
    public virtual IEnumerable<T> Get(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties.Split(new char[]
                {
                    ','
                },
                StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query,
                (current,
                    includeProperty) => current.Include(includeProperty));

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }
    
    public virtual T? GetById(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(T entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = dbSet.Find(id);
        if (entityToDelete != null) Delete(entityToDelete);
    }

    public virtual void Delete(T entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}