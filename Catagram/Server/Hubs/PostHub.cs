using Microsoft.AspNetCore.SignalR;

namespace Catagram.Server.Hubs;

public class PostHub : Hub
{
	public async Task Enter(string userName, int postId)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
	}

	public async Task AddMessageToChat(int postId, string userName, string content, DateTime dt)
	{
		await Clients.Group(postId.ToString()).SendAsync("Message", userName, content, dt);
	}
}