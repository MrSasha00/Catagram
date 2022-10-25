using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace Catagram.Client.Components;

public partial class MainFeed : ComponentBase
{
	[Parameter]
	public List<Catagram.Shared.Model.Post> Posts { get; set; } = default!;

	[Parameter]
	public bool IsEdit { get; set; }

	private void DeletePost(Catagram.Shared.Model.Post post)
	{
		Posts.Remove(post);
		StateHasChanged();
	}
}