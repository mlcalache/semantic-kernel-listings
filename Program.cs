// Imports
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;

// // Load configuration from secrets
// var configuration = new ConfigurationBuilder()
//     .SetBasePath(Directory.GetCurrentDirectory())
//     .AddUserSecrets<Program>()
//     .Build();

// string? modelId = configuration["SemanticKernel:ModelId"];
// string? endpoint = configuration["SemanticKernel:Endpoint"];
// string? apiKey = configuration["SemanticKernel:ApiKey"];

// Console.ForegroundColor = ConsoleColor.DarkYellow;
// Console.WriteLine($"Model: {modelId}");



// // Setup dependency injection container\
// var services = new ServiceCollection();

// // Add core services
// services.AddSingleton<IConfiguration>(configuration);

// // Register data source
// var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "real_estate_data.json");
// services.AddSingleton<IRealEstateRepository>(new RealEstateRepository(dataPath));  // Register repository with the interface
// services.AddSingleton<IRealEstateSearchService, RealEstateSearchService>(); // Register service with interface
// // services.AddSingleton<IListingPlugin, ListingPlugin>();  // Register plugin with the interface

// services.AddLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Debug));

// // Register Semantic Kernel and chat completion
// services.AddSingleton(sp =>
// {
//     var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

//     var builder = Kernel.CreateBuilder();

//     builder.Services.AddSingleton(loggerFactory);

//     builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

//     var kernel = builder.Build();

//     kernel.Plugins.AddFromObject(sp.GetRequiredService<IListingPlugin>(), "ListingPlugin");

//     return kernel;
// });


var app = KernelHostBuilder.Build(args);

using var scope = app.Services.CreateScope();

// Get services
var kernel = scope.ServiceProvider.GetRequiredService<Kernel>();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Create a chat history
var history = new ChatHistory();

history.AddSystemMessage("You are a strict validator. Never fabricate data. If information is missing, just state which fields are missing. Do not make anything up.");

// Execution settings
// Optional: control temperature and other settings
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
    Temperature = 0,
    TopP = 1,
    MaxTokens = 1000
};

KernelArguments kernelArguments;

// Start the chat loop
string? userInput;
do
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput)) break;

    // Create KernelArguments for the userInput
    kernelArguments = new KernelArguments
    {
        { "data", userInput } // "data" should match the parameter name in your plugin
    };

    // var validationResult = await validateFunc.InvokeAsync(kernel, kernelArguments);
    // Console.ForegroundColor = ConsoleColor.Yellow;
    // Console.WriteLine("Validator > " + validationResult);

    // // optionally decide whether to continue or not
    // if (validationResult.ToString().Contains("Incomplete"))
    //     continue;

    history.AddUserMessage(userInput);

    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Assistant > " + result);

    history.AddMessage(result.Role, result.Content ?? string.Empty);

} while (true);
