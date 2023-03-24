using OpenAI;
using OpenAI.Chat;

namespace Restruct.Cli.OpenAI;

public class Chat
{
    public OurRequest[] Prompts =
    {
        new("sender", file => $"""
                Analysiere diesen Text. Von welcher Organisation wurde der Text verfasst? Antwort darf maximal 5 Wörter umfassen.

                Datei: Stellungname.txt
                Inhalt:
                {File.ReadAllText(file.FullName)}
                """),

        new("sender_group", file => $"""
            Analysiere diesen Text. Von welcher Organisation wurde der Text verfasst? Antwort darf maximal 5 Wörter umfassen.

            Datei: Stellungname.txt
            Inhalt:
            {File.ReadAllText(file.FullName)}
            """),

        new("sentiment", file => $"""
            what sentiment between -1 to +1 would you attribute to this response  
            {File.ReadAllText(file.FullName)}
            """),

        new("summary", file => $"""
            Fasse den Text zusammen.

            Datei: Stellungname.txt
            Inhalt:
            {File.ReadAllText(file.FullName)}
            """)
    };

    public async Task<ChatResponse> Prompt(IEnumerable<ChatPrompt> chatPrompts, double? temperature, int? number = null)
    {
        var api = new OpenAIClient();

        var chatRequest =
            new ChatRequest(chatPrompts, user: GetType().FullName, temperature: temperature, number: number);

        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;


        return result;
    }

    public record OurRequest(string Attribute, Func<FileInfo, string> Prompt);
}