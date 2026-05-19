namespace Miroku.Ollama;

public class OllamaSettings
{
    public string BaseAddress { get; set; } = "http://localhost:11434/";

    public string ApiKey { get; set; } = string.Empty;
}