using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.ChatCompletion;

public static class KernelHostBuilder
{
    public static IHost Build(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.None);
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                // var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                // config.AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);
                config.AddUserSecrets<Program>();
            })
            .ConfigureServices((context, services) =>
            {
                string? modelId = context.Configuration["SemanticKernel:ModelId"];
                string? endpoint = context.Configuration["SemanticKernel:Endpoint"];
                string? apiKey = context.Configuration["SemanticKernel:ApiKey"];

                var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "real_estate_data.json");
                services.AddSingleton<IRealEstateRepository>(new RealEstateRepository(dataPath));  // Register repository with the interface
                services.AddSingleton<IRealEstateSearchService, RealEstateSearchService>(); // Register service with interface
                services.AddSingleton<IListingPlugin, ListingPlugin>();  // Register plugin with the interface

                services.AddLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Debug));

                if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(modelId))
                {
                    Console.WriteLine("Missing Semantic Kernel configuration.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Model: {modelId}");

                var kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.AddAzureOpenAIChatCompletion(
                    deploymentName: modelId,
                    endpoint: endpoint,
                    apiKey: apiKey);
                var kernel = kernelBuilder.Build();
                
                // Build intermediate service provider to resolve plugin dependency
                var provider = services.BuildServiceProvider();
                var listingPlugin = provider.GetRequiredService<IListingPlugin>();
                kernel.Plugins.AddFromObject(listingPlugin, "ListingPlugin");

                services.AddSingleton(kernel);
            })
            .Build();
    }
}
