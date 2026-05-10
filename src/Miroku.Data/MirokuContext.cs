using Microsoft.EntityFrameworkCore;
using Miroku.Data.Entities;

namespace Miroku.Data;

public class MirokuContext(DbContextOptions<MirokuContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Message> Messages { get; set; }
}