using Microsoft.EntityFrameworkCore;
using TestZadanie4.Models;

namespace TestZadanie4.Models.Db
{
    public class ConnectDb : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public ConnectDb()
        {
            Database.EnsureCreated();
        }


        public ConnectDb(DbContextOptions<ConnectDb> options) : base(options) { }
    }
}
