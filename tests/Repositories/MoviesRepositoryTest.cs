using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.Data;
using MovieAPI.Models;
using System.Threading.Tasks;

namespace MovieAPI.Test
{
    [TestClass]
    public class MoviesRepositoryTest
    {
        private readonly DbContextOptions<MovieAPIDataContext> _options;
        public MoviesRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<MovieAPIDataContext>().UseSqlite(CreateInMemoryDatabase()).Options;
        }
        [TestMethod]
        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            using (var context = new MovieAPIDataContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                
                //Creating Movies
                var movie1 = new Movie { Id = 1, Name = "Movie1" };
                var movie2 = new Movie { Id = 2, Name = "Movie2" };
                var movie3 = new Movie { Id = 3, Name = "Movie3" };
                var movie4 = new Movie { Id = 4, Name = "Movie4" };
                
                context.Add(movie1);
                context.Add(movie2);
                context.Add(movie3);
                context.Add(movie4);

                await context.SaveChangesAsync();
            }
        }
        [TestCleanup]
        public async Task TestCleanupAsync()
        {
            using (var context = new MovieAPIDataContext(_options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.DisposeAsync();
            }
        }
        
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection($"Filename=:memory:");

            connection.Open();
            
            return connection;
        }
    }
}
