using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain;
using Timesheets.Storage;
using Timesheets.Storage.Interfaces;

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
		public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _repository.FindByIdAsync(id, cancellationToken);
			if (result is Person person)
			{
				if (person.Id == -1)
					return NotFound();
				return Ok(person);
			}

			return BadRequest(result);
		}

		[HttpGet("persons/search")]
		public async Task<IQueryable<Person>> FindByName([FromQuery] string searchTerm, CancellationToken cancellationToken, [FromQuery] int skip = 1, [FromQuery] int take = 50)
		{
			return await _repository.FindByNameAsync(new GetParams { PageNumber = skip, PageSize = take }, searchTerm, cancellationToken);
		}

		[HttpGet("persons")]
		public async Task<IQueryable<Person>> FindAll([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
		{
			return await _repository.FindAllAsync(new GetParams { PageNumber = skip, PageSize = take }, cancellationToken);
		}


		[HttpPost("persons")]
		public async Task AddAsync([FromBody] Person request, CancellationToken cancellationToken) =>
			 await _repository.AddAsync(request, cancellationToken);

		[HttpPut("persons")]
		public async Task UpdateAsync([FromBody] Person request, CancellationToken cancellationToken) =>
			await _repository.UpdateAsync(request, cancellationToken);

		[HttpDelete("persons/{id}")]
		public async Task DeleteAsync([FromRoute] int id, CancellationToken cancellationToken) =>
			await _repository.DeleteAsync(id, cancellationToken);
	}
}
