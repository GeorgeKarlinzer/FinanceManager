using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;
using TestEF.Data;
using TestEF.Models;

namespace TestEF
{
    public class DbHelper
    {
        private static readonly string connectionString;

        static DbHelper()
        {
            var builder = new ConfigurationBuilder();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(path, "appsettings.json");
            builder.SetBasePath(path);
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        private static ApplicationDbContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }

        public static List<ExpenseCategory> GetCategories()
        {
            using var context = GetContext();
            return context.ExpenseCategory.ToList();
        }
    }
}
