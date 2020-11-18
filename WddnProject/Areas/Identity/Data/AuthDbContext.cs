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
                    .WithMany(gm => gm.Groups)
                    .HasForeignKey(g => g.GroupMemberId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AppUserGroupMember>()
                    .HasKey(ag => new { ag.AppUserId, ag.GroupMemberId });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Exam> Exams { get; set; }
    }
}
