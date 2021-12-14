using Microsoft.EntityFrameworkCore;
using PostcodeSearch.Db.Models;

namespace PostcodeSearch.Db
{
    public class PostcodeDbContext:DbContext
    {
        public PostcodeDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<PostcodeEntry> Postcodes { get; set; }
    }
}