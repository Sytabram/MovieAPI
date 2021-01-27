using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public interface ICategoriesRepository
    {
        Task<int> AddMovieToCategory(int categoryId, int movieId);
        Task<Category> CreateAsync(CreateCategoryDto categoryToCreate);
        Task<int> Delete(int id);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
        Task<CategoryDetailViewModel> GetSingle(int id);
        Task<int> RemoveMovieFromCategory(int categoryId, int movieId);
        Task<List<CategorySummaryViewModel>> Search(string name);
        Task<Category> UpdateAsync(int id, UpdateCategoryDto categoryToUpdate);
    }
}