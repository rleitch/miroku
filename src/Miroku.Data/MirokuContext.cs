using Microsoft.EntityFrameworkCore;
using Miroku.Data.Entities;

namespace Miroku.Data;

public class MirokuContext(DbContextOptions<MirokuContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            b.HasMany(u => u.Conversations).WithOne(c => c.User).HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Conversation>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            b.HasMany(c=>c.Messages).WithOne(m=>m.Conversation).HasForeignKey(c => c.ConversationId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Message>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
        });

        base.OnModelCreating(modelBuilder);
    }
}