using BSA_WebAPI.Models;
using BSA_WebAPI.Repos;
using BSA_WebAPI.Repos.Impl;

namespace BSA_WebAPI.Services.Impl;

public class UnitOfWork : IUnitOfWork
{
    private readonly BsaContext _context;

    public UnitOfWork(BsaContext context)
    {
        _context = context;
    }
    public IRepository<T> GetRepo<T>() where T : Entity
    {
        return new Repository<T>(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task<int> AsyncSave()
    {
        var result = await _context.SaveChangesAsync();
        return result;
    }

    #region Disposable

    // ReSharper disable once InconsistentNaming
    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
    
}