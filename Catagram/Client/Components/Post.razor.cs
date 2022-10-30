using Catagram.Client.Service;
using Catagram.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Refit;

namespace Catagram.Client.Components;

public partial class Post
{
	[Parameter]
	public Catagram.Shared.Model.Post CurrentPost { get; set; } = default!;

	[Parameter]
	public bool IsEdit { get; set; }

	[Parameter]
	public EventCallback<Catagram.Shared.Model.Post> OnDelete { get; set; }

	[Inject]
	public IPostController PostController { get; set; } = default!;

	[Inject]
	public INavigationService NavigationService { get; set; } = default!;

	[Inject]
	private IJSRuntime JsRuntime { get; set; } = default!;

	private string _uri = string.Empty;

	private HubConnection? _hubConnection;
	private string[] messages = Array.Empty<string>();
	private string message = string.Empty;

	protected override async Task OnInitializedAsync()
	{
		var photo = await PostController.GetFile(CurrentPost.PathPhoto);
		_uri = $"data:image/png;base64,{photo}";
	}

	private async Task DeletePost(int postId)
	{
		var confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Вы точно хотите удалить?");
		if (confirmed)
		{
			await PostController.Delete(CurrentPost.Id);
		}

		if (OnDelete.HasDelegate)
		{
			await OnDelete.InvokeAsync(CurrentPost);
		}
	}
}