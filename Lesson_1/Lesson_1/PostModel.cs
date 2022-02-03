namespace Lesson_1
{
	public class PostModel
	{
		public override string ToString() => $"{userId}\n{id}\n{title}\n{body}\n";

		public uint userId { get; set; }
		public uint id { get; set; }
		public string title { get; set; }
		public string body { get; set; }
	}
}
