using BSA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace BSA_WebAPI.Repos.Impl;

public class Repository<T>: IRepository<T> where T : Entity
{
    private readonly BsaContext _context;

    public Repository(BsaContext context)
    {
        _context = context;
    }
    public Task<T> GetAsync(int id)
    {
        return Task.Run(async () =>
        {
            var item =  await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id ==id);
            if(item is null)
                throw new InvalidOperationException("There are no item with such id");
            return item;
        });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public Task<T> AddAsync(T item)
    {
        return Task.Run(async () =>
        {
            await _context.Set<T>().AddAsync(item);
            return item;
        });
    }

    public async Task DeleteAsync(T item)
    {
         await Task.Run(() => { _context.Set<T>().Remove(item); });
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Run(async () =>
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null)
                throw new InvalidOperationException("Can't delete item that not presented in database");
            _context.Set<T>().Remove(item);
        });
    }

    public async Task UpdateAsync(T item)
    {
        await Task.Run(() =>
        {
            _context.Set<T>().Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        });
    }
}