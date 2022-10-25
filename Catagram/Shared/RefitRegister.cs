using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Catagram.Shared;

public static class RefitRegister
{
	public static void AddCatagramServer(this IServiceCollection services, string baseUrl, RefitSettings refitSettings)
	{
		Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(type => type.IsInterface)
			.Where(type => type.GetMethods().Any(methodInfo => methodInfo.GetCustomAttribute<GetAttribute>() != null || methodInfo.GetCustomAttribute<PostAttribute>() != null))
			.ToList()
			.ForEach(type =>
				services
					.AddRefitClient(type, refitSettings)
					.ConfigureHttpClient(config => config.BaseAddress = new Uri(baseUrl)));
	}
}