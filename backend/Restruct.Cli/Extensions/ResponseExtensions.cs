using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenAI.Chat;
using OpenAI.Completions;

namespace Restruct.Cli.Extensions;



public static class ResponseExtensions
{
    
 
    static readonly JsonSerializerOptions _chatSerializerOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        TypeInfoResolver = ChatResponseSerializerContext.Default,
        WriteIndented = true
    };
    
    static readonly JsonSerializerOptions _completionSerializerOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        TypeInfoResolver = ComletionResponseSerializerContext.Default,
        WriteIndented = true
    };
    
    public static string ToJson(this ChatResponse response)
    {
        return JsonSerializer.Serialize(response, _chatSerializerOptions);
    }
    
    public static string ToJson(this CompletionResult response)
    {
        return JsonSerializer.Serialize(response, _completionSerializerOptions);
    }
 

  

}

[JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Serialization )]
[JsonSerializable(typeof(ChatResponse))]
internal partial class ChatResponseSerializerContext : JsonSerializerContext
{
    
}

[JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(CompletionResult))]
internal partial class ComletionResponseSerializerContext : JsonSerializerContext
{
}