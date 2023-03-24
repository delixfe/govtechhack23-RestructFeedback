using OpenAI;
using OpenAI.Chat;
using Restruct.Cli.Input;

namespace Restruct.Cli.OpenAI;

public record OurRequest(string Attribute, Func<FileInfo, string> Prompt);


public static class Chat
{
    public static OurRequest[] Prompts =
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
    

    public static async Task<ChatResponse> Prompt(FileInfo fileInfo, OurRequest ourRequest, double? temperature = null, int? number = null)
    {
        var api = new OpenAIClient();

        var content = ourRequest.Prompt(fileInfo);

        var chatRequest =
            new ChatRequest(new ChatPrompt[]{new ChatPrompt("user", content)}, user: "i_am_legion", temperature: temperature, number: number);

        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;


        return result;
    }

}