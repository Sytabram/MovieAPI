using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public interface ICategoriesService
    {
        Task<bool> AddMovieToCategory(AddMovieToCategoryDto addMovieToCategory);
        Task<Category> CreateAsync(CreateCategoryDto categoryToCreate);
        Task<bool> Delete(int id);
        Task<List<CategorySummaryViewModel>> GetAll(string filterByName);
        Task<CategoryDetailViewModel> GetSingle(int id);
        Task<bool> RemoveMovieFromCategory(RemoveMovieFromCategoryDto removeMovieFromCategory);
        Task<Category> UpdateAsync(int id, UpdateCategoryDto categoryToUpdate);
    }
}