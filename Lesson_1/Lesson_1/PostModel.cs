namespace Lesson_1
{
	public class PostModel
	{
		public override string ToString() => $"{UserId}\n{Id}\n{Title}\n{Body}\n";

		public uint UserId { get; set; }
		public uint Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
	}
}
