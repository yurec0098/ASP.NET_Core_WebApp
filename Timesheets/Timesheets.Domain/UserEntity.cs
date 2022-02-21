namespace Timesheets.Domain
{
	public class UserEntity : BaseEntity<int>
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string MiddleName { get; set; } = string.Empty;
	}
}
