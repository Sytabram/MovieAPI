using MovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Data
{
    public class MovieAPIDataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Studio> Studios { get; set; }
        public MovieAPIDataContext(DbContextOptions<MovieAPIDataContext> options)
            : base(options) 
        {
        
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder) 
        // {
        //     modelBuilder.Entity<Movie>(entity =>
        //     {
        //         entity.HasMany(m => m.Category)
        //             .WithMany(c => c.Movies);
        //         
        //         entity.HasMany(m => m.Studio)
        //             .WithMany(c => c.Movies);
        //     });
        // }
    }
}