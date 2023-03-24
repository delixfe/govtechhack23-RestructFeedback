using OpenAI;
using OpenAI.Chat;
using Restruct.IntegrationTests.Extensions;

namespace Restruct.IntegrationTests.OpenAITests;

[TestFixture]
public class PromptTests
{
    [Test]
    public async Task Summary()
    {
        var consultation = Fixtures.EcflVnBundesgesetzD;
        var submission = Fixtures.VernehmlassungBe20210331;
        var api = new OpenAIClient();
        var chatPrompts = new List<ChatPrompt>
        {
            new ChatPrompt("system", "You are a helpful assistant."),
            new ChatPrompt("user", $"Der folgende Text ist ein Entwurf für die Revision eines Gesetzes."),
            new ChatPrompt("user", consultation),
            new ChatPrompt("user", $"Dazu haben wir die folgende Stellungnahme erhalten."),
            new ChatPrompt("user", submission),
            new ChatPrompt("user", $"Fasse die Stellungnahme zusammen."),
        };
        
        var chatRequest = new ChatRequest(chatPrompts);
        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;
        
        TestContext.WriteLine();
        foreach (var choice in result.Choices)
        {
            TestContext.WriteLine();
            TestContext.WriteLine($"Message: [{choice.Index}] {choice.Message?.Role}");
            TestContext.WriteLine(choice.Message?.Content);
        }
        TestContext.WriteLine();
        TestContext.WriteLine(result.ToJson());
        TestContext.WriteLine();
        Assert.IsNotNull(result);
        Assert.That(result.Object, Is.EqualTo("chat.completion"));
    }
}