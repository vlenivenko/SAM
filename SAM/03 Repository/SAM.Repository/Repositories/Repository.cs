using Microsoft.EntityFrameworkCore;
using SAM.Repository.Contexts;
using SAM.Repository.Repositories.Interfaces;

namespace SAM.Repository.Repositories
{
    /// <inheritdoc/>
    public class Repository : IRepository
    {
        protected readonly Context _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Repository(Context context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
