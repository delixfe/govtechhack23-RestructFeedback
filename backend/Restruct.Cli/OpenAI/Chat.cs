using OpenAI;
using OpenAI.Chat;

using Restruct.Cli.Model;

namespace Restruct.Cli.OpenAI;

public record Prompt(string Attribute, Func<PromptInput, string> InterpolateFunc);

public static class Chat {
    public static Prompt[] Prompts = {
        new(
            "sender",
            input => $"""
                Analysiere diesen Text. Von welcher Organisation wurde der Text verfasst? Antwort darf maximal 5 Wörter umfassen.

                Datei: Stellungname.txt
                Inhalt:
                {input.Submission}
                """
        ),
        new(
            "sender_group",
            input => $"""
            Analysiere diesen Text. Von welcher Organisation wurde der Text verfasst? Antwort darf maximal 5 Wörter umfassen.

            Datei: Stellungname.txt
            Inhalt:
            {input.Submission}
            """
        ),
        new(
            "sentiment",
            input => $"""
            what sentiment between -1 to +1 would you attribute to this response  
            {input.Submission}
            """
        ),
        new(
            "summary",
            input => $"""
            Fasse den Text zusammen.

            Datei: Stellungname.txt
            Inhalt:
            {input.Submission}
            """
        ),
    };

    public static async Task<ChatResponse> Prompt(
        PromptInput input,
        Prompt prompt,
        double? temperature = null,
        int? number = null) {
        var api = new OpenAIClient();

        var content = prompt.InterpolateFunc(input);

        var chatRequest =
            new ChatRequest(new ChatPrompt[] {
                new ChatPrompt("user", content),
            }, user: "i_am_legion", temperature: temperature, number: number);

        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;


        return result;
    }
}
