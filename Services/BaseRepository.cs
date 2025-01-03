
using Microsoft.EntityFrameworkCore;
using TentBooking.Models;

namespace TentBooking.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class

    {
        private readonly TentInventoryDbContext _dbContext;
        private readonly DbSet<T> _values;
        public BaseRepository(TentInventoryDbContext dbContext) 
        {
            _dbContext = dbContext;
            _values = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _values.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var data = await _values.FindAsync(id);
            if (data == null)
            {
                return "Entity not found";
            }
            _values.Remove(data);
            await _dbContext.SaveChangesAsync();
            return "Delete Succesfully";
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result= await _values.FindAsync(id);
            if(result==null)
                throw new KeyNotFoundException($"Booking with ID {id} not found.");
            return result;
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _values.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _values.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
