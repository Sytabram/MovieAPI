using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using System.Linq;
using Microsoft.VisualBasic;

namespace MovieAPI.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MovieAPIDataContext _context;
        public MoviesRepository(MovieAPIDataContext context)
        {
            _context = context;
        }

        public async Task<MovieDto> AddMovie(CUMovieDto newMovie)
        {
            var movieDb = new Movie();
            movieDb.Name = newMovie.Name;
            movieDb.Year = newMovie.Year;
            movieDb.Description = newMovie.Description;
            movieDb.Rating = newMovie.Rating;
            movieDb.Time = newMovie.Time;
            movieDb.Category = await GetCategoriesById(newMovie.CategoryID);
            movieDb.Studio = await GetCastsById(newMovie.StoudioID);

            _context.Movies.Add(movieDb);
            await _context.SaveChangesAsync();

            return await GetSingleMovie(movieDb.Id);
        }
        public async Task<List<Category>> GetCategoriesById(List<int> idList)
        {
           return await _context.Categories.Where(t => idList.Contains(t.Id)).ToListAsync();
        }
        public async Task<List<Studio>> GetCastsById(List<int> idList)
        {
           return await _context.Studios.Where(t => idList.Contains(t.Id)).ToListAsync();
        }
        public async Task<int> DeleteMovie(int id)
        {
            _context.Movies.Remove(await _context.Movies.SingleAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Movies.AnyAsync(c => c.Id == id);
        }
        public async Task<bool> ExistsByName(string name)
        {
            return _context.Movies.Any(c => c.Name.Contains(name));
        }
        
        public async Task<List<MovieDto>> GetAllMovie()
        {
            return await _context.Movies
            .Include(x => x.Category)
            .Include(x => x.Studio)
            .Select(t => new MovieDto
            {
                Id = t.Id,
                Name = t.Name,
                Poster = t.Poster,
                Year = t.Year,
                Description = t.Description,
                Rating = t.Rating,
                Time = t.Time,
                Category = t.Category.Select(c => new CategoryDto{
                    Id = c.Id,
                    Name = c.Name,
                }).ToList(),
                Studio = t.Studio.Select(c => new StudioDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Creation_date = c.Creation_date,
                }).ToList()
            })
            .ToListAsync();
        }
        public async Task<MovieDto> GetSingleMovie(int id)
        {
            return await _context.Movies
             .Include(x => x.Category)
             .Include(x => x.Studio)
             .Select(t => new MovieDto() 
             {
                 Id = t.Id,
                 Name = t.Name,
                 Poster = t.Poster,
                 Year = t.Year,
                 Description = t.Description,
                 Rating = t.Rating,
                 Time = t.Time,
                 Category = t.Category.Select(c => new CategoryDto{
                     Id = c.Id,
                     Name = c.Name,
                 }).ToList(),
                 Studio = t.Studio.Select(c => new StudioDto
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Country = c.Country,
                     Creation_date = c.Creation_date,
                 }).ToList()
             }).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<MovieDto> UpdateMovie(int id, CUMovieDto model)
        {
            var movie = await _context.Movies
                .Include(x => x.Category)
                .Include(x => x.Studio)
                .FirstOrDefaultAsync(c => c.Id == id);

            movie.Name = model.Name;
            movie.Year = model.Year;
            movie.Description = model.Description;
            movie.Rating = model.Rating;
            movie.Time = model.Time;
            movie.Category = await GetCategoriesById(model.CategoryID);
            movie.Studio = await GetCastsById(model.StoudioID);

            await _context.SaveChangesAsync();
            return await GetSingleMovie(movie.Id);
        }
        public async Task<bool> SetPoster(int id, byte[] image)
        {
            var movieDB = _context.Movies.Find(id);
            movieDB.Poster = image;
            return (await _context.SaveChangesAsync()) == 1;
        }
        public async Task<byte[]> GetPoster(int id)
        {
            return (await _context.Movies.FindAsync(id)).Poster;
        }

        public async Task<List<Movie>> Search(string name)
        {
            return _context.Movies.Where(c => c.Name.Contains(name)).ToList();
        }
    }
}