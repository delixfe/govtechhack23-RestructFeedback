using System.Text.Json;
using System.Text.Json.Serialization;

using OpenAI;

namespace Restruct.Cli.OpenAI;

public class Metrics {
    ulong _completionToken;
    ulong _count;
    ulong _promptTokens;
    ulong _totalTokens;

    [JsonInclude]
    public ulong PromptToken => _promptTokens;

    [JsonInclude]
    public ulong CompletionTokens => _completionToken;

    [JsonInclude]
    public ulong TotalTokens => _totalTokens;

    [JsonInclude]
    public ulong Count => _count;

    public void Add(Usage usage) {
        Interlocked.Add(ref _promptTokens, (ulong)usage.PromptTokens);
        Interlocked.Add(ref _completionToken, (ulong)usage.CompletionTokens);
        Interlocked.Add(ref _totalTokens, (ulong)usage.TotalTokens);
        Interlocked.Increment(ref _count);
    }

    public void Add(Metrics metrics) {
        Interlocked.Add(ref _promptTokens, metrics._promptTokens);
        Interlocked.Add(ref _completionToken, metrics.CompletionTokens);
        Interlocked.Add(ref _totalTokens, metrics.TotalTokens);
        Interlocked.Add(ref _count, metrics.Count);
    }

    public void WriteToJsonTextWriter(Utf8JsonWriter writer) {
        writer.WritePropertyName("_usage"u8);
        writer.WriteStartObject();
        writer.WriteNumber("count"u8, _count);
        writer.WriteNumber("prompt_tokens"u8, _promptTokens);
        writer.WriteNumber("completion_tokens"u8, _completionToken);
        writer.WriteNumber("total_tokens"u8, _totalTokens);
        writer.WriteEndObject();
    }
}
