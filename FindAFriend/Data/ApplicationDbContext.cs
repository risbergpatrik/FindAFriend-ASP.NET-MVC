using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FindAFriend.Models;

namespace FindAFriend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FindAFriend.Models.Profile> Profile { get; set; }
        public DbSet<FindAFriend.Models.FriendRequests> FriendRequests { get; set; }
        public DbSet<FindAFriend.Models.Friends> Friends { get; set; }
        public DbSet<FindAFriend.Models.Message> Message { get; set; }
    }
}
