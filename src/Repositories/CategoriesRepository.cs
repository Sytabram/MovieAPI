using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MovieAPIDataContext _context;
        public CategoriesRepository(MovieAPIDataContext context)
        {
            _context = context;
        }

        public Task<CategoryDetailViewModel> GetSingle(int id)
        {
            return _context.Categories.Include(t => t.Movies)
                                 .Select(t => new CategoryDetailViewModel
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     Movies = t.Movies.Select(c => new MovieSummaryViewModel
                                     {
                                         Id = c.Id,
                                         Name = c.Name
                                     })
                                 }).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category> UpdateAsync(int id, UpdateCategoryDto categoryToUpdate)
        {
            var category = await _context.Categories.FirstAsync(c => c.Id == id);

            category.Name = categoryToUpdate.Name;

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> CreateAsync(CreateCategoryDto categoryToCreate)
        {
            var categoryDb = new Category();
            categoryDb.Name = categoryToCreate.Name;

            _context.Categories.Add(categoryDb);
            await _context.SaveChangesAsync();

            return categoryDb;
        }

        public Task<List<CategorySummaryViewModel>> Search(string name)
        {
            return _context.Categories.Where(c => string.IsNullOrWhiteSpace(name) || c.Name.Contains(name)).Select(t => 
            new CategorySummaryViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task<int> Delete(int id)
        {
            _context.Categories.Remove(await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddMovieToCategory(int categoryId, int movieId)
        {
            var categoryDb = await _context.Categories.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == categoryId);

            categoryDb.Movies.Add(_context.Movies.Find(movieId));

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveMovieFromCategory(int categoryId, int movieId)
        {
            var categoryDb = await _context.Categories.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == categoryId);

            categoryDb.Movies.Remove(categoryDb.Movies.First(c => c.Id == movieId));

            return await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsById(int id)
        {
            return _context.Categories.AnyAsync(c => c.Id == id);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _context.Categories.AnyAsync(c => c.Name == name);
        }
    }
}