namespace SAM.Repository.Repositories.Interfaces
{
    /// <summary>
    /// High-level repository representation
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Returns entity list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync<T>() where T : class;

        /// <summary>
        /// Returns entity by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetByIdAsync<T>(int id) where T : class;

        /// <summary>
        /// Add entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
