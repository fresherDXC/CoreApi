using Microsoft.EntityFrameworkCore;

namespace CoreApi.Models
{
    public class userContext : DbContext
    {
        public userContext(DbContextOptions<userContext> options)
            : base(options)
        {
        }

        public DbSet<userItem> users { get; set; }
    }
}