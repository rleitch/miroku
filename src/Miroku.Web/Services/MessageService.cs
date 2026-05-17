using Miroku.Data;
using Miroku.Web.ViewModels;
using Miroku.Ollama;
using Miroku.Core.Extensions;
using Miroku.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Miroku.Web.Services
{
    public class MessageService(MirokuContext context, OllamaClient ollamaClient)
    {
        private readonly MirokuContext _context = context;
        public event Func<MessageViewModel, Task>? StateUpdated;
        public readonly OllamaClient _ollamaClient = ollamaClient;
        private readonly string _conversationNamePrompt = PromptHelper.GetPrompt("ConversationName");

        //public async Task SaveAsync(UserViewModel? userModel, MessageViewModel messageViewModel)
        //{
        //    userModel.ThrowIfNull(nameof(userModel));

        //    var conversation = new Conversation();
        //    if(userModel?.ActiveConversation?.Id is null)
        //    {
        //        var user = await _context.Users.FindAsync(userModel?.Id);
        //        user?.Conversations.Add(conversation);
        //    }
        //    else
        //    {
        //        conversation = await _context.Conversations
        //            .Include(c => c.Messages)
        //            .FirstAsync(c => c.Id == userModel.ActiveConversation.Id);
        //    }

        //    var message = new Message
        //    {
        //        Content = messageViewModel.Content
        //    };

        //    conversation.Messages.Add(message);
        //    await _context.SaveChangesAsync();
            
        //    var response = await _ollamaClient.GenerateAsync(_conversationNamePrompt, messageViewModel.Content);
        //}
    }
}
