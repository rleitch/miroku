using Microsoft.EntityFrameworkCore;
using Miroku.Data;
using Miroku.Data.Entities;
using Miroku.Web.ViewModels;

namespace Miroku.Web.Services;

public class UserService(IDbContextFactory<MirokuContext> DbFactory)
{
    public event Func<UserViewModel, Task>? UserLoaded;

    public UserViewModel? User { get; set; }

    public async Task LoadUserAsync(Guid userId)
    {
        await GetUser(userId);
        if (User is null)
        {
            await CreateUser(userId);
        }
    }

    private async Task GetUser(Guid? userId)
    {
        if (!userId.HasValue)
        {
            return;
        }

        using var mirokuContext = await DbFactory.CreateDbContextAsync();
        var user = await mirokuContext.Users
            .Include(u => u.Conversations)
            .FirstOrDefaultAsync(u => u.Id == userId.Value);

        if(user == null)
        {
            return;
        }

        User = new UserViewModel(user);
        var latestConversation = await mirokuContext.Conversations
            .Include(c => c.Messages)
            .OrderByDescending(c => c.Messages.Max(m => m.DateCreated))
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        if (latestConversation != null)
        {
            User.ActiveConversation = new ConversationViewModel(latestConversation);
        }
        UserLoaded?.Invoke(User);
    }

    private async Task CreateUser(Guid userId)
    {
        using var mirokuContext = await DbFactory.CreateDbContextAsync();
        var user = new User { Id = userId };
        await mirokuContext.Users.AddAsync(user);
        await mirokuContext.SaveChangesAsync();
        User = new UserViewModel(user);
        UserLoaded?.Invoke(User);
    }
}
