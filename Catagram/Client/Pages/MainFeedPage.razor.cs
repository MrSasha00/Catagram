using Catagram.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Post = Catagram.Shared.Model.Post;

namespace Catagram.Client.Pages;

[Route("posts")]
public partial class MainFeedPage : ComponentBase
{
	[Inject]
	private IPostController PostController { get; set; } = default!;

	private List<Post>? _posts;

	protected override async Task OnParametersSetAsync()
	{
		_posts = (await PostController.GetAll())
			.OrderByDescending(p => p.CreatedDt)
			.ToList();
	}
}