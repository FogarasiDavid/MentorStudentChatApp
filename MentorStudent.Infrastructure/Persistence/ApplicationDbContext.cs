using MentorStudent.Domain.Aggregates;
using MentorStudent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorStudent.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<MentorProfile> MentorProfiles => Set<MentorProfile>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
            });
        });

        modelBuilder.Entity<MentorProfile>(builder =>
        {
            builder.HasKey(x => x.UserId);
        });

        modelBuilder.Entity<StudentProfile>(builder =>
        {
            builder.HasKey(x => x.UserId);
        });

        modelBuilder.Entity<Chat>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MentorId).IsRequired();
            builder.Property(x => x.StudentId).IsRequired();

            builder.HasIndex(x => new { x.MentorId, x.StudentId })
                   .IsUnique(); 
        });

        modelBuilder.Entity<Message>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChatId).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.Content).IsRequired();
        });

        modelBuilder.Entity<Notification>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Message).IsRequired();
        });
    }
}
