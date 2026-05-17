using Miroku.Data.Entities;

namespace Miroku.Web.ViewModels;

public class UserViewModel(User User) : BaseViewModel(User)
{
    public ConversationViewModel ActiveConversation { get; set; } = new ConversationViewModel();

    public Dictionary<Guid, ConversationViewModel> Conversations { get; set; } =
        User?.Conversations?.OrderByDescending(c => c.DateCreated)?.ToDictionary(c => c.Id, c => new ConversationViewModel(c)) ?? [];
}