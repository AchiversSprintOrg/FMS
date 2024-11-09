using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using Finance_Api.DTO;

namespace Finance_Api.Models
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext()
        {

        }
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=FinanceManagementSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public DbSet<Finance_Api.DTO.ExpenseDTO> ExpenseDTO { get; set; } = default!;
    }
}
