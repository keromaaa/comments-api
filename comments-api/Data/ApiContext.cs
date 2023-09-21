using Microsoft.EntityFrameworkCore;
using comments_api.Models;

namespace comments_api.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        { 
              
        }



    }
}
