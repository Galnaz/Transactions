using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionManagement.Infrastructure.Data.Entities;

namespace TransactionManagement.Infrastructure
{
    public class TransactionManagementContext: DbContext
    {
        public TransactionManagementContext(DbContextOptions<TransactionManagementContext> options) : base(options)
        {
        }

        protected TransactionManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEntity>(
                builder =>
                builder.ToTable("transactionRecords"));

        }
    }
}
