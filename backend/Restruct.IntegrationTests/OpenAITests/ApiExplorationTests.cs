using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

using Restruct.Cli.Extensions;

namespace Restruct.IntegrationTests.OpenAITests;

[TestFixture]
public class ApiExplorationTests
{
    [Test]
    public async Task Chat()
    {
        var api = new OpenAIClient();
        var chatPrompts = new List<ChatPrompt>
        {
            new("system", "You are a helpful assistant."),
            new("user", "Who won the world series in 2020?"),
            new("assistant", "The Los Angeles Dodgers won the World Series in 2020."),
            new("user", "Where was it played?")
        };

        var chatRequest = new ChatRequest(chatPrompts);
        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest)!;

        TestContext.WriteLine();
        TestContext.WriteLine(result.ToJson());
        TestContext.WriteLine();

        Assert.IsNotNull(result);
        Assert.That(result.Object, Is.EqualTo("chat.completion"));

        // response
        // {
        //     "id": "chatcmpl-6xNrkal5SyMQnW8KrvRc6hkXiafKn",
        //     "object": "chat.completion",
        //     "created": 1679611112,
        //     "model": "gpt-3.5-turbo-0301",
        //     "usage": {
        //         "prompt_tokens": 57,
        //         "completion_tokens": 51,
        //         "total_tokens": 108
        //     },
        //     "choices": [
        //     {
        //         "message": {
        //             "role": "assistant",
        //             "content": "The 2020 World Series between the Los Angeles Dodgers and the Tampa Bay Rays was played at Globe Life Field in Arlington, Texas. It was the first time that the World Series was played at a neutral site, due to the COVID-19 pandemic."
        //         },
        //         "delta": null,
        //         "finish_reason": "stop",
        //         "index": 0
        //     }
        //     ]
        // }
    }

    [Test]
    public async Task Completions()
    {
        var api = new OpenAIClient();
        var result =
            await api.CompletionsEndpoint.CreateCompletionAsync(
                "1, 1, 2, 3, 5,",
                temperature: 0.1,
                model: Model.Davinci
            );

        TestContext.WriteLine();
        TestContext.WriteLine(result.ToJson());
        TestContext.WriteLine();

        Assert.IsNotNull(result);
        Assert.That(result.Object, Is.EqualTo("text_completion"));

        // response
        // 8, 13, 21, 34, 55, 89]\n\n\#for
    }
}
