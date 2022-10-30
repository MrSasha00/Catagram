using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace Catagram.Client.Pages;

[Route("/post/{postId:int}")]
public partial class PostView : ComponentBase
{
	[Inject]
	public IPostController PostController { get; set; } = default!;

	[Parameter]
	public int PostId { get; set; }

	private Post? _post;
	private bool _isLoaded;

	protected override async Task OnInitializedAsync()
	{
		_post = await PostController.GetPostById(PostId);
		_isLoaded = true;
	}
}