using Microsoft.EntityFrameworkCore;
using project_x_da.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_x_da.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserFirebaseSession> UserFirebaseSessions { get; set; }
        public DbSet<UserForgotPassword> UserForgotPasswords { get; set; }
        public DbSet<UserRememberToken> UserRememberTokens { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
