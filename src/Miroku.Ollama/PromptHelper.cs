namespace Miroku.Ollama;

public sealed class PromptHelper
{
    public static string GetPrompt(string prompt)
    {
        return ReadEmbedded($"Miroku.Ollama.Prompts.{prompt}.txt");
    }

    private static string ReadEmbedded(string resourceName)
    {
        var asm = typeof(PromptHelper).Assembly;
        using var s = asm.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Missing resource: {resourceName}");
        using var r = new StreamReader(s);
        return r.ReadToEnd();
    }
}