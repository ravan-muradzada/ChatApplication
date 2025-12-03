using ChatApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired();

                entity.HasIndex(u => u.Username)
                    .IsUnique();

                entity.Property(u => u.HashedPassword)
                    .IsRequired();
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(m => m.SenderUserId)
                    .IsRequired();

                entity.Property(m => m.Text)
                    .IsRequired();
            });

            //modelBuilder.Entity<Chat>(entity =>
            //{
            //    entity.Property(c => c.ChatName)
            //        .IsRequired();
            //});

            modelBuilder.Entity<ChatUser>()
                .HasKey(cu => new { cu.ChatId, cu.UserId });

            modelBuilder.Entity<ChatUser>()
                .HasOne(cu => cu.Chat)
                .WithMany(c => c.ChatUsers);

            modelBuilder.Entity<ChatUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.ChatUsers);
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
