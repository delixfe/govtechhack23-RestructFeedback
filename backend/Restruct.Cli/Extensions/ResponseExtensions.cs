using System.Text.Encodings.Web;
using System.Text.Json;

using OpenAI.Chat;
using OpenAI.Completions;

namespace Restruct.Cli.Extensions;

public static class ResponseExtensions {
    static readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true, };

    public static string ToJson(this ChatResponse response) {
        return JsonSerializer.Serialize(response, _jsonSerializerOptions);
    }

    public static string ToJson(this CompletionResult response) {
        return JsonSerializer.Serialize(response, _jsonSerializerOptions);
    }
}
