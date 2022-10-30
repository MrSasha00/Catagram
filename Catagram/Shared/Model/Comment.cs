namespace Catagram.Shared.Model;

public class Comment : BaseEntity
{
	public string Content { get; set; } = string.Empty;
	public DateTime CreatedDt { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
	public int PostId { get; set; }
	public Post Post { get; set; }
}