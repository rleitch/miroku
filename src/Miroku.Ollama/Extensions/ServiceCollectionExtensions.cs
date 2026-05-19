using Microsoft.Extensions.DependencyInjection;

namespace Miroku.Ollama.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOllamaClient(this IServiceCollection services, OllamaSettings settings)
    {
        services.AddHttpClient<OllamaClient>(client =>
        {
            client.BaseAddress = new Uri(settings.BaseAddress);
            client.DefaultRequestHeaders.Add("X-API-KEY", settings.ApiKey);
        });

        return services;
    }
}