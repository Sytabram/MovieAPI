using System.Collections.Generic;
using System.Linq;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MovieAPIDataContext _context;
        
        public MoviesService(MovieAPIDataContext context)
        {
            _context = context;
        }

        public IList<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }
        public Movie GetSingle(int id)
        {
            return _context.Movies.FirstOrDefault(c => c.Id == id);
        }

        public Movie Update(int id, MoviePatchViewModel model)
        {
            var movie = _context.Movies.FirstOrDefault(c => c.Id == id);

            movie.Name = model.Name;
            movie.Category = model.Category;
            movie.Studio = model.Studio;

            _context.SaveChanges();

            return movie;
        }

        public Movie AddMovie(Movie newMovie)
        {
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return newMovie;
        }

        public IList<Movie> Search(string name)
        {
            return _context.Movies.Where(c => c.Name.Contains(name)).ToList();
        }

        public void Delete(int id)
        {
            _context.Movies.Remove(_context.Movies.FirstOrDefault(c => c.Id == id));
            _context.SaveChanges();
        }

        public void SetPoster(int id, byte[] image)
        {
            var movie = _context.Movies.Find(id);
            movie.Poster = image;
            _context.SaveChanges();
        }

        public byte[] GetPoster(int id)
        {
            return _context.Movies.Find(id).Poster;
        }

        public bool ExistsById(int id)
        {
            return _context.Movies.Any(c => c.Id == id);
        }

        public bool ExistsByName(string name)
        {
            return _context.Movies.Any(c => c.Name.Contains(name));
        }
    }
}