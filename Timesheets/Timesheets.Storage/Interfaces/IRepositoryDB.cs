namespace Timesheets.Storage.Interfaces
{
	public interface IRepositoryDB<T>
	{
		Task<T> GetEntityAsync(int id, CancellationToken cancellationToken);
		Task<IQueryable<T>> GetAllEntityAsync(CancellationToken cancellationToken);
		Task<bool> EntityExistsAsync(int id, CancellationToken cancellationToken);

		Task AddEntityAsync(T entity, CancellationToken cancellationToken);
		Task UpdateEntityAsync(T entity, CancellationToken cancellationToken);
		Task DeleteEntityAsync(T entity, CancellationToken cancellationToken);
	}
}
