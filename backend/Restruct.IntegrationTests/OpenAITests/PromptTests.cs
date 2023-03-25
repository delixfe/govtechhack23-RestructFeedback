using OpenAI;
using OpenAI.Chat;
using Restruct.Cli.Extensions;

namespace Restruct.IntegrationTests.OpenAITests;

[TestFixture]
public class PromptTests
{
    
    [Test]
    [Ignore("The message is too long. We will have to wait for GPT4.")]
    public async Task Summary()
    {
        var consultation = Fixtures.EcflVnBundesgesetzD;
        var submission = Fixtures.VernehmlassungBe20210331;

        var prompts = new List<ChatPrompt>
        {
            new ChatPrompt("system", "Du bist ein präziser Assistent und drückst Dich so kurz wie möglich aus."),
            new ChatPrompt("user", $"""
                Ich stelle die folgenden 2 Dateien und ihre Dateinamen zur Verfügung.
        
                Datei: entwurf.txt
                Inhalt:
                {consultation}
            
                Datei: stellungnahme.txt
                Inhalt:
                {submission}
               
                Fasse den Inhalt der Datei entwurf.txt in 1 Satz zusammen.
                """),
            new ChatPrompt("assistant","""Die Datei "entwurf.txt" enthält den Entwurf eines Bundesgesetzes."""),
            new ChatPrompt("user", """Fasse den Inhalt der Datei "stellungnahme.txt" in 1 Satz zusammen."""),
            
        };

       await Prompt(prompts, number: null);
    }


    public async Task<ChatResponse> Prompt(IEnumerable<ChatPrompt> chatPrompts, int? number = null )
    {
        var api = new OpenAIClient();
        
        var chatRequest = new ChatRequest(chatPrompts, user: GetType().FullName, temperature: 0, number: number);
        
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
        
        return result;
    }
}