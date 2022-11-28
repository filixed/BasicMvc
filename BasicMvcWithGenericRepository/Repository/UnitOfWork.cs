using BasicMvcWithGenericRepository.Models;

namespace BasicMvcWithGenericRepository.Repository;

public class UnitOfWork<T> : IUnitOfWork<T> where T : class
{
    private readonly PostgreDbContext? context;
    private IGenericRepository<T>? repository;

    private bool disposed = false;

    public UnitOfWork(PostgreDbContext context, IGenericRepository<T> repository)
    {
        this.context = context;
        this.repository = repository;
    }

    public IGenericRepository<T> GetGenericRepository
    {
        get
        {
            if (this.repository == null)
            {
                this.repository = new GenericRepository<T>(context);
            }
            return repository;
        }
    }
    
    public void Save()
    {
        context.SaveChanges();
    }

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}