using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace Catagram.Client.Pages;

[Route("/posts/{login}")]
public partial class UserFeedPage
{
	[Parameter]
	public string? Login { get; set; }

	[Inject]
	public IPostController PostController { get; set; } = default!;

	private List<Post>? _posts;

	protected override async Task OnParametersSetAsync()
	{
		if (Login != null)
		{
			_posts = (await PostController.GetByLogin(Login))
				.OrderByDescending(p => p.CreatedDt)
				.ToList();
		}
	}
}