using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public interface IMoviesRepository
    {
        Task<int> AddMovieToCategory(int categoryId, int movieId);
    }
}