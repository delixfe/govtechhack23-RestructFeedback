using OpenAI;
using OpenAI.Chat;

using Restruct.Cli.Model;

namespace Restruct.Cli.OpenAI;

public record PromptDefinition(string Attribute, Func<PlaceholderAccessor, string> InterpolateFunc);

public static class Chat {
    public static readonly PromptDefinition[] Prompts = {
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
        OpenAIClient openAiClient,
        PlaceholderAccessor placeholderAccessor,
        PromptDefinition promptDefinition,
        double? temperature = null,
        int? number = null) {
        var content = promptDefinition.InterpolateFunc(placeholderAccessor);
        var chatPrompt = new ChatPrompt("user", content);
        var chatPrompts = new[] { chatPrompt, };
        var chatRequest =
            new ChatRequest(
                chatPrompts,
                user: "i_am_legion",
                temperature: temperature,
                number: number
            );

        var result = await openAiClient.ChatEndpoint.GetCompletionAsync(chatRequest)!;


        return result;
    }
}
