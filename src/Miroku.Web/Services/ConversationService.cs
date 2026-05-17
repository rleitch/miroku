using Microsoft.EntityFrameworkCore;
using Miroku.Core;
using Miroku.Data;
using Miroku.Data.Entities;
using Miroku.Ollama;
using Miroku.Ollama.Models.Requests;
using Miroku.Web.ViewModels;
using System.Data;

namespace Miroku.Web.Services;

public class ConversationService(IDbContextFactory<MirokuContext> DbFactory, OllamaClient ollamaClient)
{
    public event Func<MessageViewModel, Task>? MessageSaved;
    public event Func<ConversationViewModel, Task>? ConversationSaved;
    public event Action<ConversationViewModel>? ConversationLoaded;
    public event Action<Guid>? ConversationDeleted;
    public event Action<ConversationViewModel>? ConversationCreated;
    public readonly OllamaClient _ollamaClient = ollamaClient;
    private readonly string _conversationNamePrompt = PromptHelper.GetPrompt("ConversationName");
    private readonly string _defaultPrompt = PromptHelper.GetPrompt("DefaultPrompt");

    public async Task SaveAsync(Guid userId, Guid conversationId, MessageViewModel messageView)
    {
        using var mirokuContext = await DbFactory.CreateDbContextAsync();
        var converation = await mirokuContext.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == conversationId && c.UserId == userId);

        var conversationCreated = false;
        if (converation is null)
        {
            var user = await mirokuContext.Users.FindAsync(userId);
            converation = new Conversation()
            {
                Id = conversationId,
                Name = await NameConversation(messageView.Content)
            };
            user?.Conversations.Add(converation);
            conversationCreated = true;
        }

        Message message = new() { Id = messageView.Id, Content = messageView.Content, Role = messageView.Role };
        converation.Messages.Add(message);

        if (mirokuContext.ChangeTracker.HasChanges())
        {
            await mirokuContext.SaveChangesAsync();
        }

        if (conversationCreated)
        {
            ConversationCreated?.Invoke(new ConversationViewModel(converation));
        }
    }

    public async IAsyncEnumerable<string> StreamResponse(ConversationViewModel? conversationViewModel, bool think = true)
    {
        ArgumentNullException.ThrowIfNull(conversationViewModel);

        var request = new ChatRequest
        {
            Stream = true
        };

        request.Messages.Add(new ChatRequestMessage("system", _defaultPrompt));
        var chatRequestMessages = conversationViewModel?.Messages?.Where(m => !string.IsNullOrWhiteSpace(m.Content)).Select(MapToChatRequestMessage)?.ToArray() ?? [];
        if (chatRequestMessages?.Length > 0)
        {
            request.Messages.AddRange(chatRequestMessages);
        }
        await foreach (var chunk in _ollamaClient.StreamResponse(request))
        {
            yield return chunk;
        }
    }

    private static ChatRequestMessage MapToChatRequestMessage(MessageViewModel messageViewModel)
    {
        var role = string.Empty;
        switch (messageViewModel.Role)
        {
            case MessageRole.Assistant:
                role = "assistant";
                break;
            case MessageRole.System:
                role = "system";
                break;
            case MessageRole.User:
                role = "user";
                break;
        }
        return new ChatRequestMessage(role, messageViewModel.Content);
    }

    public async Task LoadConversationAsync(Guid userId, Guid conversationId)
    {
        using var mirokuContext = await DbFactory.CreateDbContextAsync();
        var conversation = (await mirokuContext.Conversations.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == conversationId && c.UserId == userId)) ?? new Conversation();
        ConversationLoaded?.Invoke(new ConversationViewModel(conversation));
    }

    public async Task DeleteConversation(Guid userId, Guid conversationId)
    {
        using var mirokuContext = await DbFactory.CreateDbContextAsync();
        await mirokuContext.Conversations.Where(c => c.Id == conversationId).ExecuteDeleteAsync();
        ConversationDeleted?.Invoke(conversationId);
    }

    public async Task<string> NameConversation(string userPrompt)
    {
        var generateRequest = new GenerateRequest()
        {
            System = _conversationNamePrompt,
            Prompt = userPrompt
        };
        return await _ollamaClient.GenerateAsync(generateRequest);
    }
}
