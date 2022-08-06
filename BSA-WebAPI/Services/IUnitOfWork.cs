using BSA_WebAPI.Models;
using BSA_WebAPI.Repos;

namespace BSA_WebAPI.Services;

public interface IUnitOfWork: IDisposable
{
    public IRepository<T> GetRepo<T>() where T : Entity;
    public void Save();
    public Task<int> AsyncSave();
}