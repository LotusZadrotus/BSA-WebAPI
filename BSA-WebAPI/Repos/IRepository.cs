namespace BSA_WebAPI.Repos;

public interface IRepository<T>
{
    public Task<T> GetAsync(int id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> AddAsync(T item);
    public Task DeleteAsync(T item);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(T item);
}