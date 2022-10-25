using Catagram.Client.Service;
using Microsoft.AspNetCore.Components;

namespace Catagram.Client.Pages;

[Route("")]
public partial class MainPage : ComponentBase
{
	[Inject]
	public INavigationService NavigationService { get; set; } = default!;
}