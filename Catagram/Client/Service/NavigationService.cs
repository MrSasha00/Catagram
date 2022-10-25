using Microsoft.AspNetCore.Components;

namespace Catagram.Client.Service;

public class NavigationService : INavigationService
{
	private readonly NavigationManager _navigationManager;

	public NavigationService(NavigationManager navigationManager)
	{
		_navigationManager = navigationManager;
	}

	public void ToLogin() => _navigationManager.NavigateTo("login");
	public void ToPostsByLogin(string login) => _navigationManager.NavigateTo($"posts/{login}");
	public void ToPosts() => _navigationManager.NavigateTo("posts");
	public void ToMyPosts() => _navigationManager.NavigateTo("myPosts");
	public void ToEditPost(int editPostId) => _navigationManager.NavigateTo($"posts/{editPostId}/edit");
	public void ToRoot() => _navigationManager.NavigateTo("");
	public void ToCreatePost() => _navigationManager.NavigateTo("createPost");
	
	
}