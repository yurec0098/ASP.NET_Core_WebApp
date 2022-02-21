#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheets.Domain;
using Timesheets.Storage;
using Timesheets.Storage.Interfaces;

namespace Timesheets.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly DbEntityRepository<User> _context;

		public UsersController(IRepositoryDB<User> context)
		{
			_context = (DbEntityRepository<User>)context;
		}

		// GET: api/[controller]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUser(CancellationToken cancellationToken)
		{
			var users = await _context.GetAllEntityAsync(cancellationToken);
			return await users.ToListAsync();
		}

		// GET: api/[controller]/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id, CancellationToken cancellationToken)
		{
			var user = await _context.GetEntityAsync(id, cancellationToken);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		// PUT: api/[controller]/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user, CancellationToken cancellationToken)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}

			try
			{
				await _context.UpdateEntityAsync(user, cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.EntityExistsAsync(id, cancellationToken))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/[controller]
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User usser, CancellationToken cancellationToken)
		{
			await _context.AddEntityAsync(usser, cancellationToken);

			return CreatedAtAction("GetUser", new { id = usser.Id }, usser);
		}

		// DELETE: api/[controller]/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
		{
			var user = await _context.GetEntityAsync(id, cancellationToken);
			if (user == null)
			{
				return NotFound();
			}

			await _context.DeleteEntityAsync(user, cancellationToken);

			return NoContent();
		}
	}
}
