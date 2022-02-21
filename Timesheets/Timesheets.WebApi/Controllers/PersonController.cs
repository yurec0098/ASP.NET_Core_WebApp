using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain;
using Timesheets.Storage;

namespace Timesheets.WebApi.Controllers
{
	[Route("api")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private IRepository<Person> _repository;

		public PersonController(IRepository<Person> repository)
		{
			_repository = repository;
		}


		[HttpGet("persons/{id}")]
		public async Task<ActionResult<Person>> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			return await _repository.FindByIdAsync(id, cancellationToken) ?? (ActionResult<Person>)NotFound();
		}

		[HttpGet("persons/search")]
		public async Task<IEnumerable<Person>> FindByName([FromQuery] string searchTerm, CancellationToken cancellationToken, [FromQuery] int skip = 1, [FromQuery] int take = 50)
		{
			return await _repository.FindByNameAsync(new GetParams { PageNumber = skip, PageSize = take }, searchTerm, cancellationToken);
		}

		[HttpGet("persons")]
		public async Task<IEnumerable<Person>> FindAll([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
		{
			return await _repository.FindAllAsync(new GetParams { PageNumber = skip, PageSize = take }, cancellationToken);
		}


		[HttpPost("persons")]
		public async Task<ActionResult<Person>> AddAsync([FromBody] Person request, CancellationToken cancellationToken)
		{
			await _repository.AddAsync(request, cancellationToken);
			return CreatedAtAction("GetById", new { id = request.Id }, request);
		}

		[HttpPut("persons")]
		public async Task<IActionResult> UpdateAsync([FromBody] Person request, CancellationToken cancellationToken)
		{
			if (await _repository.FindByIdAsync(request.Id, cancellationToken) == null)
				return NotFound();
			
			await _repository.UpdateAsync(request, cancellationToken);
			return NoContent();
		}

		[HttpDelete("persons/{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
		{
			if(await _repository.FindByIdAsync(id, cancellationToken) is Person person)
				await _repository.DeleteAsync(person, cancellationToken);
			else
				return NotFound();

			return NoContent();
		}
	}
}
