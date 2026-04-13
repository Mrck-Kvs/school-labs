namespace SocialGraphBot.Repositories;

/// <summary>
/// Interface de base pour tous les repositories
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByKeyAsync(string key);
    Task<T> UpsertAsync(T entity);
    Task<bool> DeleteAsync(string key);
    Task<IEnumerable<T>> GetAllAsync();
}