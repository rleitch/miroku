using Miroku.Core;
using Miroku.Data.Entities;

namespace Miroku.Web.ViewModels;

public class MessageViewModel : BaseViewModel
{
    public MessageViewModel(string content)
    {
        Content = content;
    }

    public MessageViewModel(Message message)
        : base(message)
    {
        Content = message.Content;
        Role = message.Role;
    }

    public string Content { get; set; } = string.Empty;

    public MessageRole Role { get; set; } = MessageRole.User;
}