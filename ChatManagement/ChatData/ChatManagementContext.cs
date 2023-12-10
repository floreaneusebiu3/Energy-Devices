using ChatDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatData;

public class ChatManagementContext: DbContext
{
    private readonly IConfiguration _config;

    public ChatManagementContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _config["Url:ConnectionString"];
        optionsBuilder.UseSqlServer(
            connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasOne(m => m.SenderUser)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderUserId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.DestionationUser)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.DestionationUserId)
            .IsRequired(false);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Group)
            .WithMany(g => g.Messages)
            .HasForeignKey(m => m.GroupId)
            .IsRequired(false);

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Admin)
            .WithMany(u => u.GroupsAsAdmin)
            .HasForeignKey(g => g.AdminId);

        base.OnModelCreating(modelBuilder);
    }

}