using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public interface IStudiosService
    {
        Task<StudioDetailViewModel> GetSingle(int id);
        Task<Studio> UpdateAsync(int id, UpdateStudioDto studioToUpdate);
        Task<Studio> CreateAsync(CreateStudioDto studioToCreate);
        Task<List<StudioSummaryViewModel>> GetAll(string filterByName);
        Task<bool> Delete(int id);
        Task<bool> AddMovieToStudio(AddMovieToStudioDto addMovieToStudio);
        Task<bool> RemoveMovieFromStudio(RemoveMovieFromStudioDto removeMovieFromStudio);
    }
}