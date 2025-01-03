namespace TentBooking.Services
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<string> DeleteAsync(int id);
    }
}
