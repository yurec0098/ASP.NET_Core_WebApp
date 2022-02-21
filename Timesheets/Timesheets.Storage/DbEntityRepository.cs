using Microsoft.EntityFrameworkCore;
using Timesheets.Domain;
using Timesheets.Storage.Interfaces;

namespace Timesheets.Storage
{
	public class DbEntityRepository<TEntity> : IRepositoryDB<TEntity> where TEntity : UserEntity, new()
	{
		private readonly TimesheetsDb _context;

		public DbEntityRepository(TimesheetsDb context)
		{
			_context = context;
		}

		/// <summary> Get all Entities </summary>
		public async Task<IQueryable<TEntity>> GetAllEntityAsync(CancellationToken cancellationToken)
		{
			return await Task.Run(() => _context.Set<TEntity>().AsQueryable());
		}

		/// <summary> Get Entity by ID </summary>
		public async Task<TEntity> GetEntityAsync(int id, CancellationToken cancellationToken)
		{
			return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
		}

		/// <summary> Entity Exists by ID </summary>
		public async Task<bool> EntityExistsAsync(int id, CancellationToken cancellationToken)
		{
			return await _context.Set<TEntity>().AnyAsync(e => e.Id == id, cancellationToken);
		}

		/// <summary> Upadte Entity </summary>
		public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		/// <summary> Add Entity </summary>
		public async Task AddEntityAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_context.Set<TEntity>().Add(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}

		/// <summary> Delete Entity by ID </summary>
		public async Task DeleteEntityAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
