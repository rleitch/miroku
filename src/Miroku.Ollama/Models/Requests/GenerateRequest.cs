using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models.Requests;

public class GenerateRequest : BaseRequest
{
    public GenerateRequest()
    {
        Stream = false;
        Think = false;
    }

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    [JsonPropertyName("system")]
    public string System { get; set; } = string.Empty;
}
