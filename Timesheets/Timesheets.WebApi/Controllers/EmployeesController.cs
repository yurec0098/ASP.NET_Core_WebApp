using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheets.Domain;
using Timesheets.Storage;
using Timesheets.Storage.Interfaces;

namespace Timesheets.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly DbEntityRepository<Employee> _context;

		public EmployeesController(IRepositoryDB<Employee> context)
		{
			_context = (DbEntityRepository<Employee>)context;
		}

		// GET: api/[controller]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(CancellationToken cancellationToken)
		{
			var employees = await _context.GetAllEntityAsync(cancellationToken);
			return await employees.ToListAsync(cancellationToken);
		}

		// GET: api/[controller]/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployee(int id, CancellationToken cancellationToken)
		{
			return await _context.GetEntityAsync(id, cancellationToken) ?? (ActionResult<Employee>)NotFound();
		}

		// PUT: api/[controller]/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEmployee(int id, Employee employee, CancellationToken cancellationToken)
		{
			if (id != employee.Id)
				return BadRequest();

			try
			{
				await _context.UpdateEntityAsync(employee, cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.EntityExistsAsync(id, cancellationToken))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// POST: api/[controller]
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Employee>> PostEmployee(Employee employee, CancellationToken cancellationToken)
		{
			await _context.AddEntityAsync(employee, cancellationToken);

			return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
		}

		// DELETE: api/[controller]/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
		{
			if (await _context.GetEntityAsync(id, cancellationToken) is Employee employee)
				await _context.DeleteEntityAsync(employee, cancellationToken);
			else
				return NotFound();

			return NoContent();
		}
	}
}
