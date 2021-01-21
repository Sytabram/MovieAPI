using System.Collections.Generic;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public interface IMovieService
    {
        Movie AddMovie(Movie newMovie);
        void Delete(int id);
        bool ExistsById(int id);
        bool ExistsByName(string name);
        IList<Movie> GetAll();
        Movie GetSingle(int id);
        IList<Movie> Search(string name);
        Movie Update(int id, MoviePatchViewModel model);
        void SetPoster(int id, byte[] image);
        byte[] GetPoster(int id);
    }
}