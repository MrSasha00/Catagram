using Catagram.Client.Service;
using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace Catagram.Client.Components;

public partial class CreatePost : ComponentBase
{
	[Inject]
	private IPostController PostController { get; set; } = default!;

	[Inject]
	private INavigationService NavigationService { get; set; } = default!;

	[CascadingParameter]
	private Task<AuthenticationState> AuthenticationState { get; set; } = default!;

	private CreatePostDTO CreatePostDto { get; set; } = new();

	private EditContext? EditContext { get; set; }

	private string _uri = default!;
	private string _photo = default!;

	protected override void OnInitialized()
	{
		EditContext = new EditContext(CreatePostDto);
	}

	async Task HandleSelection(InputFileChangeEventArgs inputFileChangeEventArgs)
	{
		var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 1024, 1024);

		await using Stream imageStream = image.OpenReadStream(1920 * 1920 * 10);
		using MemoryStream ms = new();
		await imageStream.CopyToAsync(ms);

		_photo = Convert.ToBase64String(ms.ToArray());
		CreatePostDto.PathPhoto = _photo;
		_uri = $"data:image/png;base64,{_photo}";

		StateHasChanged();
	}

	private async Task Post()
	{
		if (EditContext != null && EditContext.Validate())
		{
			await PostController.Create(CreatePostDto);
			NavigationService.ToRoot();
		}
	}
}