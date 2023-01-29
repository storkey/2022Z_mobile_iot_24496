using Lab1_Bosianek.Rest.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Bosianek.Rest.Context
{
    public class AzureDbContext: DbContext
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options) { }

        protected AzureDbContext()
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
