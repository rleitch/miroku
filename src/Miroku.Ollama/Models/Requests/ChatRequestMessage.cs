using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models.Requests;

public class ChatRequestMessage(string role, string content)
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = role;

    [JsonPropertyName("content")]
    public string Content { get; set; } = content;
}