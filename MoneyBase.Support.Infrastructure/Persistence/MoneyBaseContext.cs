using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Infrastructure.Persistence
{
    public class MoneyBaseContext : DbContext
    {
        public MoneyBaseContext(DbContextOptions<MoneyBaseContext> options) : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>().HasKey(a => a.Id);
            modelBuilder.Entity<ChatSession>().HasKey(c => c.Id);
        }
    }
}
