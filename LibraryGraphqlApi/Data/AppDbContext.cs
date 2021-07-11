using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryGraphqlApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
