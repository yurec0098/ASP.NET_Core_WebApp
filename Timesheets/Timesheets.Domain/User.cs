namespace Timesheets.Domain
{
	public sealed class User : UserEntity
	{
		public string Comment { get; set; } = string.Empty;
	}
}
