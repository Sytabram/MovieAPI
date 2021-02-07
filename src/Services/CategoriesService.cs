using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<CategoryDetailViewModel> GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var CategoryDb = await _categoriesRepository.GetSingle(id);

            return CategoryDb;
        }

        public async Task<Category> UpdateAsync(int id, UpdateCategoryDto categoryToUpdate)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (categoryToUpdate == null)
                throw new ArgumentNullException(nameof(categoryToUpdate));

            if (categoryToUpdate.Name.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(categoryToUpdate.Name), categoryToUpdate.Name, "Category name length cannot be greater than 35.");

            if (!await _categoriesRepository.ExistsById(id))
                throw new DataNotFoundException($"Category Id:{id} doesn't exists.");

            if (await _categoriesRepository.ExistsByName(categoryToUpdate.Name))
                throw new ArgumentException(nameof(categoryToUpdate.Name), $"Category {categoryToUpdate.Name} already exists.");

            return await _categoriesRepository.UpdateAsync(id, categoryToUpdate);
        }

        public async Task<Category> CreateAsync(CreateCategoryDto categoryToCreate)
        {
            if (categoryToCreate == null)
                throw new ArgumentNullException(nameof(categoryToCreate));

            if (categoryToCreate.Name.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(categoryToCreate.Name), categoryToCreate.Name, "Category name length cannot be greater than 32.");

            if (await _categoriesRepository.ExistsByName(categoryToCreate.Name))
                throw new ArgumentException(nameof(categoryToCreate.Name), $"Category {categoryToCreate.Name} already exists.");

            var modelDb = await _categoriesRepository.CreateAsync(categoryToCreate);

            return modelDb;
        }

        public Task<List<CategorySummaryViewModel>> GetAll(string filterByName)
        {
            if (filterByName?.Length < 4)
                throw new ArgumentOutOfRangeException("Category name length must be greater than 3.");

            return _categoriesRepository.Search(filterByName);
        }

        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var result = await _categoriesRepository.Delete(id);

            //If result == 1, one entity has been deleted from the database
            if (result == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> AddMovieToCategory(AddMovieToCategoryDto addMovieToCategory)
        {
            if (addMovieToCategory == null)
                throw new ArgumentNullException(nameof(addMovieToCategory));

            if (addMovieToCategory.CategoryId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMovieToCategory.CategoryId), addMovieToCategory.CategoryId, "Category Id cannot be lower than 1.");

            if (addMovieToCategory.MovieId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMovieToCategory.MovieId), addMovieToCategory.MovieId, "Movie Id cannot be lower than 1.");

            if (!await _categoriesRepository.ExistsById(addMovieToCategory.CategoryId))
                throw new DataNotFoundException($"Category Id:{addMovieToCategory.CategoryId} doesn't exists.");

            // Ici, si on avait un CharacterRepository on devrait checker si le CharacterId existe dans la db
            // if (!_characterRepository.ExistsById(addCharacterToTeam.CharacterId))
            //     throw new DataNotFoundException($"CharacterId:{addCharacterToTeam.CharacterId} doesn't exists.");

            var result = await _categoriesRepository.AddMovieToCategory(addMovieToCategory.CategoryId, addMovieToCategory.MovieId);

            if (result == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> RemoveMovieFromCategory(RemoveMovieFromCategoryDto removeMovieFromCategory)
        {
            if (removeMovieFromCategory == null)
                throw new ArgumentNullException(nameof(removeMovieFromCategory));

            if (removeMovieFromCategory.CategoryId < 1)
                throw new ArgumentOutOfRangeException(nameof(removeMovieFromCategory.CategoryId), removeMovieFromCategory.CategoryId, "Category Id cannot be lower than 1.");

            if (removeMovieFromCategory.MovieId < 1)
                throw new ArgumentOutOfRangeException(nameof(removeMovieFromCategory.MovieId), removeMovieFromCategory.MovieId, "Movie Id cannot be lower than 1.");

            if (!await _categoriesRepository.ExistsById(removeMovieFromCategory.CategoryId))
                throw new DataNotFoundException($"Category Id:{removeMovieFromCategory.CategoryId} doesn't exists.");

            // Ici, si on avait un CharacterRepository on devrait checker si le CharacterId existe dans la db
            // if (!_characterRepository.ExistsById(addCharacterToTeam.CharacterId))
            //     throw new DataNotFoundException($"CharacterId:{addCharacterToTeam.CharacterId} doesn't exists.");

            var result = await _categoriesRepository.RemoveMovieFromCategory(removeMovieFromCategory.CategoryId, removeMovieFromCategory.MovieId);

            if (result == 1)
                return true;
            else
                return false;
        }
    }
}