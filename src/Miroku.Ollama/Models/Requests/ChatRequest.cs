using System.Text.Json.Serialization;

namespace Miroku.Ollama.Models.Requests
{
    public class ChatRequest : BaseRequest
    {
        [JsonPropertyName("messages")]
        public List<ChatRequestMessage> Messages { get; set; } = [];
    }
}