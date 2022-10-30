namespace Catagram.Shared.Model;

public class CreateCommentDto
{
	public string Content { get; set; } = string.Empty;
	public DateTime CreatedDt { get; set; }
	public string UserName { get; set; }
	public int PostId { get; set; }
}