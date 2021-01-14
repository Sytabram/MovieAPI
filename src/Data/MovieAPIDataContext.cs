using MovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Data
{
    public class MovieAPIDataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        

        public MovieAPIDataContext(DbContextOptions<MovieAPIDataContext> options)
            : base(options) 
        {
        
        }
    }
}