using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models.Requests;

public abstract class BaseRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "gemma4:e4b";

    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = true;

    [JsonPropertyName("think")]
    public bool Think { get; set; } = true;

    [JsonPropertyName("keep_alive")]
    public int KeepAlive { get; set; } = -1;

    [JsonPropertyName("options")]
    public OllamaOptions Options { get; set; } = new OllamaOptions();

    public sealed class OllamaOptions
    {
        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = 1;

        [JsonPropertyName("top_p")]
        public float TopP { get; set; } = 0.95F;

        [JsonPropertyName("top_k")]
        public int TopK { get; set; } = 64;

        [JsonPropertyName("num_ctx")]
        public int MaxConversationSize { get; set; } = 32768;

        [JsonPropertyName("num_predict")]
        public int MaxResponseSize { get; set; } = 8192;
    }
}