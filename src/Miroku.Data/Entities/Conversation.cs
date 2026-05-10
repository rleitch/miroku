namespace Miroku.Data.Entities;

public class Conversation : BaseEntity
{
    public List<Message> Messages { get; set; } = [];
}
