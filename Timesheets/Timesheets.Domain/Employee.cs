namespace Timesheets.Domain
{
	public sealed class Employee : UserEntity
	{
		public string Position { get; set; } = string.Empty;
	}
}
