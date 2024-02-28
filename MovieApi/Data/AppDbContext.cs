using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data {

    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Movie> Movies {get; set;}


    }
}