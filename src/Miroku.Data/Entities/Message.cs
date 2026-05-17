using Miroku.Core;

namespace Miroku.Data.Entities;

public class Message : BaseEntity
{
    public string Content { get; set; } = string.Empty;

    public MessageRole Role { get; set; } = MessageRole.User;

    public Guid ConversationId { get; set; }

    public Conversation Conversation { get; set; }
}