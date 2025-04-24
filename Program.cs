// Import packages
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;

// Load config with secrets
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .Build();

// Read from secrets
// To add a new secret, use dotnet user-secrets set "YOUR KEY HERE" "YOUR SECRET HERE"
// You need:
//    dotnet user-secrets set "SemanticKernel:ModelId" "YORU SECRET HERE"
//    dotnet user-secrets set "SemanticKernel:Endpoint" "YORU SECRET HERE"
//    dotnet user-secrets set "SemanticKernel:ApiKey" "YORU SECRET HERE"
// To see the secrets, use dotnet user-secrets list
string? modelId = configuration["SemanticKernel:ModelId"];
string? endpoint = configuration["SemanticKernel:Endpoint"];
string? apiKey = configuration["SemanticKernel:ApiKey"];

if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(modelId))
{
    Console.WriteLine("Missing Semantic Kernel configuration.");
    return;
}

// Create a kernel with Azure OpenAI chat completion
// var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Add enterprise components
// builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

// Build the kernel
Kernel kernel = builder.Build();

// Retrieve the chat completion service
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Add a plugin
// kernel.Plugins.AddFromType<LightsPlugin>("Lights");
var repository = new RealEstateRepository("real_estate_data.json");
kernel.Plugins.AddFromObject(new ListingPlugin(repository), "ListingPlugin");

// Enable planning
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() 
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

// Create a history store the conversation
var history = new ChatHistory();

// Initiate a back-and-forth chat
string? userInput;
do {
    // Collect user input
    Console.Write("User > ");
    userInput = Console.ReadLine();

    // Add user input
    history.AddUserMessage(userInput);

    // Get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Assistant > " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);