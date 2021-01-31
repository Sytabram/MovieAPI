using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public interface IMoviesRepository
    {
        Task<MovieDto> AddMovie(CUMovieDto newMovie);
        Task<int> DeleteMovie(int id);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
        Task<List<MovieDto>> GetAllMovie();
        Task<MovieDto> GetSingleMovie(int id);
        Task<MovieDto> UpdateMovie(int id, CUMovieDto model);
        Task<bool> SetPoster(int id, byte[] image);
        Task<byte[]> GetPoster(int id);
        Task<List<Movie>> Search(string name);

    }
}