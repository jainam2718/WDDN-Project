using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WDDNProject.Areas.Identity.Data;
using WDDNProject.Models;

namespace WDDNProject.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AppUser>()
                    .HasMany<Exam>(a => a.Exams)
                    .WithOne(e => e.AppUser)
                    .IsRequired()
                    .HasForeignKey(e => e.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    

            builder.Entity<Exam>()
                    .HasOne<Group>(e => e.Group)
                    .WithMany(g => g.Exams)
                    .HasForeignKey(e => e.GroupId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AppUser>()
                    .HasMany<Group>(a => a.Groups)
                    .WithOne(g => g.AppUser)
                    .IsRequired()
                    .HasForeignKey(g => g.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                    .HasOne<GroupMember>(g => g.GroupMember)
                    .WithOne(gm => gm.Group)
                    .HasForeignKey<GroupMember>(gm => gm.GroupId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<AppUserGroupMember>()
                    .HasKey(ag => new { ag.AppUserId, ag.GroupMemberId });

            builder.Entity<Exam>()
                .HasMany<Questions>(e => e.Questions)
                .WithOne(q => q.Exam)
                .IsRequired()
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        
        public DbSet<AppUserGroupMember> AppUserGroupMembers { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Exam> Exams { get; set; }
    }
}
