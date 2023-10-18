using Microsoft.EntityFrameworkCore;
using comments_api.Models;

namespace comments_api.Data
{
    public class ApiContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        //public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        //{

        //}

        public ApiContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        
    }
}
