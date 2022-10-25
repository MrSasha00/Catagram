using System.Text.Json.Serialization;

namespace Catagram.Shared.Model;

public class Post : BaseEntity
{
	public string PathPhoto { get; set; }
	public string Description { get; set; }
	public DateTime CreatedDt { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
}