using Microsoft.EntityFrameworkCore;
using NewsParserDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NewsParserDAL
{
    public class NewsParserDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public NewsParserDbContext(string connectionString)
        {
            _connectionString = connectionString;
            //Database.EnsureCreated();
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
