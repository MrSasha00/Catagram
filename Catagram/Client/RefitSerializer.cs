using Catagram.Shared;
using Newtonsoft.Json;
using Refit;

namespace Catagram.Client;

public static class RefitSerializer
{
	public static void AddRefitSerializer(this IServiceCollection services, string baseUrl)
	{
		var refitSettings = new RefitSettings
		{
			ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects
			})
		};

		services.AddCatagramServer(baseUrl, refitSettings);
	}
}