using System.Text.Json;
using System.Text.Json.Serialization;

using OpenAI;

namespace Restruct.Cli.OpenAI;

public class Metrics {
    ulong _completionToken;
    ulong _promptTokens;
    ulong _requestCount;
    ulong _requestFailureCount;
    ulong _totalTokens;

    [JsonInclude]
    public ulong PromptToken => _promptTokens;

    [JsonInclude]
    public ulong CompletionTokens => _completionToken;

    [JsonInclude]
    public ulong TotalTokens => _totalTokens;

    [JsonInclude]
    public ulong RequestCount => _requestCount;

    [JsonInclude]
    public ulong RequestFailureCount => _requestFailureCount;

    public void Add(Usage usage) {
        Interlocked.Add(ref _promptTokens, (ulong)usage.PromptTokens);
        Interlocked.Add(ref _completionToken, (ulong)usage.CompletionTokens);
        Interlocked.Add(ref _totalTokens, (ulong)usage.TotalTokens);
        Interlocked.Increment(ref _requestCount);
    }

    public void IncrementFailed() {
        Interlocked.Increment(ref _requestCount);
        Interlocked.Increment(ref _requestFailureCount);
    }

    public void Add(Metrics metrics) {
        Interlocked.Add(ref _promptTokens, metrics._promptTokens);
        Interlocked.Add(ref _completionToken, metrics.CompletionTokens);
        Interlocked.Add(ref _totalTokens, metrics.TotalTokens);
        Interlocked.Add(ref _requestCount, metrics.RequestCount);
        Interlocked.Add(ref _requestFailureCount, metrics.RequestFailureCount);
    }

    public void WriteToJsonTextWriter(Utf8JsonWriter writer) {
        writer.WritePropertyName("_usage"u8);
        writer.WriteStartObject();
        writer.WriteNumber("request_count"u8, _requestCount);
        writer.WriteNumber("request_failure_count"u8, _requestFailureCount);
        writer.WriteNumber("prompt_tokens"u8, _promptTokens);
        writer.WriteNumber("completion_tokens"u8, _completionToken);
        writer.WriteNumber("total_tokens"u8, _totalTokens);
        writer.WriteEndObject();
    }
}
