using Miroku.Ollama.Models;
using Miroku.Ollama.Models.Requests;
using Miroku.Ollama.Models.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace Miroku.Ollama;

public class OllamaClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async IAsyncEnumerable<string> StreamResponse(ChatRequest request)
    {
        request.Stream = true;
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/chat")
        {
            Content = JsonContent.Create(request)
        };
        var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        string? chunk;
        while ((chunk = await reader.ReadLineAsync()) != null)
        {
            var chatResponse = JsonSerializer.Deserialize<ChatResponse>(chunk);
            yield return chatResponse.Message.Content;
        }
    }

    public async Task<string> ChatAsync(ChatRequest request)
    {
        var res = await _httpClient.PostAsJsonAsync("api/chat", request);
        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadFromJsonAsync<ChatResponse>();
        var assistant = json!.Message!.Content;

        return assistant;
    }

    public async Task<string> GenerateAsync(GenerateRequest generateRequest)
    {
        var res = await _httpClient.PostAsJsonAsync("api/generate", generateRequest);
        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadFromJsonAsync<OllamaGenerateResponse>();
        return json!.Response;
    }
}
