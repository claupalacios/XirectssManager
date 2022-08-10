using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace AccountMS.Models
{
    public partial class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
        }

        public AccountDBContext(DbContextOptions<AccountDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AccountMSDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Account");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
