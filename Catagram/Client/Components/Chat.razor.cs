using Catagram.Client.Service;
using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;

namespace Catagram.Client.Components;

public partial class Chat : ComponentBase
{
	[Inject]
	public INavigationService NavigationService { get; set; } = default!;

	[Inject]
	public ICommentController CommentController { get; set; } = default!;

	[Parameter]
	public int PostId { get; set; }

	[CascadingParameter]
	public Task<AuthenticationState> AuthenticationState { get; set; } = default!;

	private string _login = string.Empty;
	private HubConnection? _hubConnection;
	private string newMessage = string.Empty;

	private List<CreateCommentDto> _comments = new();
	
	protected override async Task OnInitializedAsync()
	{
		_comments = (await CommentController.GetCommentsByPost(PostId))
			.Select(p => new CreateCommentDto
			{
				Content = p.Content,
				CreatedDt = p.CreatedDt.ToLocalTime(),
				UserName = p.User.Login,
				PostId = p.PostId
			}).ToList();
		await Connect();
	}

	protected override async Task OnParametersSetAsync()
	{
		_login = (await AuthenticationState).User.Identity?.Name;
	}

	private async Task Connect()
	{
		_hubConnection = new HubConnectionBuilder()
			.WithUrl(NavigationService.GetPostHubUri())
			.Build();


		_hubConnection.On<string, string, DateTime>("Message", (userName, content, dt) =>
		{
			var comment = new CreateCommentDto()
			{
				Content = content,
				CreatedDt = dt,
				UserName = userName,
				PostId = PostId
			};
			_comments.Add(comment);
			StateHasChanged();
		});

		
		await _hubConnection.StartAsync();
		await _hubConnection.SendAsync("Enter",_login, PostId);
	}

	private async Task SendMessage()
	{
		if (_hubConnection != null)
		{
			var createdDt = DateTime.Now;
			var comment = new CreateCommentDto()
			{
				Content = newMessage,
				CreatedDt = createdDt,
				UserName = _login,
				PostId = PostId
			};
			await CommentController.SaveComment(comment);
			await _hubConnection.SendAsync("AddMessageToChat", PostId, _login, newMessage, createdDt);
			newMessage = string.Empty;
		}
	}
}