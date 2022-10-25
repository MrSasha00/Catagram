using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Catagram.Client.Pages;

[Route("myPosts")]
public partial class MyFeedPage
{
	[Inject]
	public IPostController PostController { get; set; } = default!;

	[CascadingParameter]
	public Task<AuthenticationState> AuthenticationState { get; set; }

	private List<Post>? _posts;

	protected override async Task OnInitializedAsync()
	{
		_posts = (await PostController.GetByLogin((await AuthenticationState).User.Identity.Name))
			.OrderByDescending(p => p.CreatedDt)
			.ToList();
	}
}