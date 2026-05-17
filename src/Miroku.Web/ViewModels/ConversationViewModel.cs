using Miroku.Data.Entities;

namespace Miroku.Web.ViewModels;

public class ConversationViewModel : BaseViewModel
{
    public ConversationViewModel()
    {
        
    }

    public ConversationViewModel(Conversation conversation)
        : base(conversation)
    {
        Name = conversation.Name;

        Messages = conversation?.Messages?
            .OrderBy(m => m.DateCreated)?
            .Select(m => new MessageViewModel(m))?
            .ToList() ?? [];
    }

    public string Name { get; set; } = string.Empty;

    public List<MessageViewModel> Messages { get; set; } = [];
}