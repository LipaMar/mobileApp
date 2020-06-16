using Microsoft.EntityFrameworkCore;
using mobileApp.Web.Models;

namespace mobileApp.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-S12G8F4\SQLEXPRESS;Database=MobileApp;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipent)
                .WithMany(s => s.MessagesReceived)
                .HasForeignKey(m => m.RecipentId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
