using BSA_WebAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace BSA_WebAPI.Services;

public interface IService<T> where T:Entity
{
    public Task<T> GetAsync(int id);
    public Task<T> AddAsync(T item);
    public Task DeleteAsync(T item);
    public Task DeleteAsync(int id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task UpdateAsync(T item);
}