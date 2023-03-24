pusing System.Text.Json.Serialization;
using OpenAI;

namespace Restruct.Cli.OpenAI;

public class Metrics
{
    private ulong promptTokens;
    private ulong completionToken;
    private ulong totalTokens;
    private ulong count;
    
    
    public void Add(Usage usage)
    {
        Interlocked.Add(ref promptTokens,(ulong)usage.PromptTokens);
        Interlocked.Add(ref completionToken,(ulong)usage.CompletionTokens);
        Interlocked.Add(ref totalTokens,(ulong)usage.TotalTokens);
        Interlocked.Increment(ref count);
    }

    [JsonInclude]
    public ulong PromptToken => promptTokens;
    [JsonInclude]
    public ulong CompletionTokens => completionToken;
    [JsonInclude]
    public ulong TotalTokens => totalTokens;
    [JsonInclude]
    public ulong Count => count;
}
