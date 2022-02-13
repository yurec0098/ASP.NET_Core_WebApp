namespace Timesheets.Storage
{
	public interface IRepository<T>
	{
		Task<T> FindByIdAsync(int id, CancellationToken cancellationToken);
		Task<IQueryable<T>> FindByNameAsync(GetParams getParams, string name, CancellationToken cancellationToken);
		Task<IQueryable<T>> FindAllAsync(GetParams getParams, CancellationToken cancellationToken);

		Task AddAsync(T entity, CancellationToken cancellationToken);
		Task UpdateAsync(T entity, CancellationToken cancellationToken);
		Task DeleteAsync(int id, CancellationToken cancellationToken);
	}
}
