using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestEF.Models;

namespace TestEF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TestEF.Models.ExpenseCategory> ExpenseCategory { get; set; }
        public DbSet<TestEF.Models.Expense> Expense { get; set; }
    }
}