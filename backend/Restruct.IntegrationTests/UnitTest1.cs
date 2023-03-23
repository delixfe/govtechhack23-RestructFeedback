using OpenAI;
using OpenAI.Chat;

namespace Restruct.IntegrationTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    // login to OpenAI  
    [Test]
    public async Task Login()
    {
        var api = new OpenAIClient();
        var chatPrompts = new List<ChatPrompt>
        {
            new ChatPrompt("system", "You are a helpful assistant."),
            new ChatPrompt("user", "Who won the world series in 2020?"),
            new ChatPrompt("assistant", "The Los Angeles Dodgers won the World Series in 2020."),
            new ChatPrompt("user", "Where was it played?"),
        };
        var chatRequest = new ChatRequest(chatPrompts);
        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;
        Assert.IsNotNull(result);
    }
}