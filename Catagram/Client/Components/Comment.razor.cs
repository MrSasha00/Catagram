using Catagram.Client.Service;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace Catagram.Client.Components;

public partial class Comment : ComponentBase
{
	[Inject]
	public INavigationService NavigationService { get; set; } = default!;

	[Parameter]
	public CreateCommentDto CommentValue { get; set; } = default!;
}