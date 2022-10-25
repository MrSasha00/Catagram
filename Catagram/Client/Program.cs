using Catagram.Client;
using Catagram.Client.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var refitBaseUrl = GetBaseUrlForRefit(builder);
builder.Services.AddRefitSerializer(refitBaseUrl);
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INavigationService, NavigationService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();

static string GetBaseUrlForRefit(WebAssemblyHostBuilder builder)
{
	var refitBaseUrl = builder.HostEnvironment.BaseAddress[0..^1];

	return refitBaseUrl;
}