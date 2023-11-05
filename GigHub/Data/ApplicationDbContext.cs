using GigHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace GigHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set;}
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; } 
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }  

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() 
            :base()
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            //Podesavanje za tabelu Attendance
            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .Property(table => table.GigId)
                .HasColumnOrder(0);


            builder.Entity<Attendance>()
                .Property(table => table.AttendeeId)
                .HasColumnOrder(1);

            builder.Entity<Attendance>()
                .HasKey(table => new { table.GigId, table.AttendeeId });
            

            //Podesavanje za tabelu Following
            builder.Entity<Following>()
                .Property(table => table.FollowerId)
                .HasColumnOrder(0);

            builder.Entity<Following>()
                .Property(table => table.FolloweeId)
                .HasColumnOrder(1);

            builder.Entity<ApplicationUser>()
                .HasMany(table => table.Followers)
                .WithOne(f => f.Followee).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<ApplicationUser>()
                .HasMany(table => table.Followees)
                .WithOne(f => f.Follower).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<Following>()
                .HasKey(table => new { table.FollowerId, table.FolloweeId });

            //Podesavanja za tabelu UserNotifications
            builder.Entity<UserNotification>()
                .Property(table => table.UserId) 
                .HasColumnOrder(0);

            builder.Entity<UserNotification>()
                .Property(table => table.NotificationId)
                .HasColumnOrder(1);

            builder.Entity<UserNotification>()
                .HasKey(table => new {table.UserId, table.NotificationId});

            builder.Entity<UserNotification>()
                .HasOne(table => table.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);


        }
    }
}