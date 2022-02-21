namespace Timesheets.Domain
{
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Company { get; set; } = string.Empty;
		public int Age { get; set; }
	}
}
