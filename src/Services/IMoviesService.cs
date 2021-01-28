using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public interface IMoviesService
    {
        Task<List<MovieDto>> GetAll();
        Task<MovieDto> GetSingle(int id);
        Task<Movie> Update(int id, CUMovieDto model);
        Task<MovieDto> AddMovie(CUMovieDto newMovie);
        Task<List<Movie>> Search(string name);
        Task<bool> Delete(int id);
        Task<bool> SetPoster(int id, byte[] image);
        Task<byte[]> GetPoster(int id);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
    }
}