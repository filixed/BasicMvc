using BasicMvcWithGenericRepository.Models;

namespace BasicMvcWithGenericRepository.Repository;

public interface IUnitOfWork<T> where T : class
{
    IGenericRepository<T> GetGenericRepository { get; }
    void Save();
}