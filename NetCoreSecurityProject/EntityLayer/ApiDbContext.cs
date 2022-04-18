﻿using Entities_HBKSOFTWARE.JwtModels;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public ApiDbContext() { }

        #region /*DbClasses*/
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q7EGB25; Initial Catalog=ApiDb;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}
