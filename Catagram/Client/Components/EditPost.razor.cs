using Catagram.Client.Service;
using Catagram.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Catagram.Client.Components;

[Route("posts/{EditingPostId:int}/edit")]
public partial class EditPost
{
	[Parameter]
	public int EditingPostId { get; set; }

	[Inject]
	public IPostController PostController { get; set; } = default!;

	[Inject]
	public INavigationService NavigationService { get; set; } = default!;
	private Catagram.Shared.Model.Post Post { get; set; } = new();
	private EditContext? EditContext { get; set; }

	private string _uri = string.Empty;

	protected override async Task OnInitializedAsync()
	{
		EditContext = new EditContext(Post);
		Post = await PostController.GetPostById(EditingPostId);

		var photo = await PostController.GetFile(Post.PathPhoto);
		_uri = $"data:image/png;base64,{photo}";
	}

	private async Task Save()
	{
		if (EditContext != null && EditContext.Validate())
		{
			await PostController.Update(Post);
			NavigationService.ToMyPosts();
		}
	}
}