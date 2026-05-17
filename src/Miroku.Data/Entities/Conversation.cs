namespace Miroku.Data.Entities;

public class Conversation : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public List<Message> Messages { get; set; } = [];

    public Guid UserId { get; set; }

    public User User { get; set; }
}
