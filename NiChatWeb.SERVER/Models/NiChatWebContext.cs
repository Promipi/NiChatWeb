using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NiChatWeb.Data;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class NiChatWebContext : DbContext
    {
        public NiChatWebContext()
        {
        }

        public NiChatWebContext(DbContextOptions<NiChatWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserChat> UserChats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Direc.SqlConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.Creation).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Body).HasMaxLength(4000);

                entity.Property(e => e.Creation).HasColumnType("datetime");

                entity.Property(e => e.FChat).HasColumnName("FChat");

                entity.Property(e => e.FUser).HasColumnName("FUser");

                entity.HasOne(d => d.FchatNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FChat)
                    .HasConstraintName("fk_numeroChat");

                entity.HasOne(d => d.FuserNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FUser)
                    .HasConstraintName("fk_numeroUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Gmail)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Username).HasMaxLength(10);
            });

            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.ToTable("User_Chat");

                entity.Property(e => e.DateJoin)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Join");

                entity.Property(e => e.FChat).HasColumnName("FChat");

                entity.Property(e => e.FUser).HasColumnName("FUser");

                entity.HasOne(d => d.FchatNavigation)
                    .WithMany(p => p.UserChats)
                    .HasForeignKey(d => d.FChat)
                    .HasConstraintName("fk_idChat");

                entity.HasOne(d => d.FuserNavigation)
                    .WithMany(p => p.UserChats)
                    .HasForeignKey(d => d.FUser)
                    .HasConstraintName("fk_idUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
