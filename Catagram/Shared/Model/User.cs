using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Catagram.Shared.Model;

public class User : BaseEntity
{
	public string Login { get; set; }

	[JsonIgnore]
	[IgnoreDataMember]
	public List<Post> Posts { get; set; } = new();
}