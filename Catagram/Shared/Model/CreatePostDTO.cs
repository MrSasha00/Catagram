using System.ComponentModel.DataAnnotations;

namespace Catagram.Shared.Model;

public class CreatePostDTO
{
	[Required]
	public string PathPhoto { get; set; }
	[Required]
	public string Description { get; set; }
}