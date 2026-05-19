using Miroku.Ollama;

namespace Miroku.Web.Configuration;

public class Settings
{
    public OllamaSettings OllamaSettings { get; set; } = new OllamaSettings();
}