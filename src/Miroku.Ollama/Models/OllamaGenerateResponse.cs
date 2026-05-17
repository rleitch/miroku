using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models;

public class OllamaGenerateResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;

    [JsonPropertyName("done")]
    public bool Done { get; set; }
}