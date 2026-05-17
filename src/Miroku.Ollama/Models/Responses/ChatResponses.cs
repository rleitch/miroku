using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models.Responses;

// --- TOP-LEVEL CONTAINER CLASS ---
public class ChatResponse
{
    [JsonPropertyName("model")]
    public string Model { get; set; }

    // It's best practice to use DateTimeOffset for ISO 8601 dates
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("message")]
    public Message Message { get; set; }

    [JsonPropertyName("done")]
    public bool Done { get; set; }

    [JsonPropertyName("done_reason")]
    public string DoneReason { get; set; }

    [JsonPropertyName("total_duration")]
    public double TotalDuration { get; set; }

    [JsonPropertyName("load_duration")]
    public double LoadDuration { get; set; }

    [JsonPropertyName("prompt_eval_count")]
    public double PromptEvalCount { get; set; }

    [JsonPropertyName("prompt_eval_duration")]
    public double PromptEvalDuration { get; set; }

    [JsonPropertyName("eval_count")]
    public double EvalCount { get; set; }

    [JsonPropertyName("eval_duration")]
    public double EvalDuration { get; set; }

    [JsonPropertyName("logprobs")]
    public List<LogProbEntry> LogProbs { get; set; }
}


// --- NESTED STRUCTURE: Message ---
public class Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("thinking")]
    public string Thinking { get; set; }

    [JsonPropertyName("tool_calls")]
    // This is an array of objects, each containing a "function"
    public List<ToolCall> ToolCalls { get; set; }

    [JsonPropertyName("images")]
    // This is an array of strings (image URLs or paths)
    public List<string> Images { get; set; }
}

// --- NESTED STRUCTURE: Tool Call ---
public class ToolCall
{
    [JsonPropertyName("function")]
    public FunctionCall Function { get; set; }
}

// --- NESTED STRUCTURE: Function Definition ---
public class FunctionCall
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    // The arguments field is an empty JSON object in the example, 
    // suggesting it can hold any arbitrary JSON object/dictionary.
    [JsonPropertyName("arguments")]
    public Dictionary<string, object> Arguments { get; set; }
}


// --- NESTED STRUCTURE: LogProbs ---
public class LogProbEntry
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("logprob")]
    public double LogProb { get; set; }

    // Byte arrays are best represented as List<byte> or byte[]
    [JsonPropertyName("bytes")]
    public List<byte> Bytes { get; set; }

    [JsonPropertyName("top_logprobs")]
    public List<TopLogProb> TopLogProbs { get; set; }
}


// --- DEEPLY NESTED STRUCTURE: Top Log Probability ---
public class TopLogProb
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("logprob")]
    public double LogProb { get; set; }

    [JsonPropertyName("bytes")]
    public List<byte> Bytes { get; set; }
}
