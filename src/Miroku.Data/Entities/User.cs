namespace Miroku.Data.Entities
{
    public class User : BaseEntity
    {
        public List<Conversation> Conversations { get; set; } = [];
    }
}