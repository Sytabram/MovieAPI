using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public interface IStudiosRepository
    {
        Task<StudioDetailViewModel> GetSingle(int id);
        Task<Studio> UpdateAsync(int id, UpdateStudioDto studioToUpdate);
        Task<Studio> CreateAsync(CreateStudioDto studioToCreate);
        Task<List<StudioSummaryViewModel>> Search(string name);
        Task<int> Delete(int id);
        Task<int> AddMovieToStudio(int studioId, int movieId);
        Task<int> RemoveMovieStudio(int studioId, int movieId);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
    }
}