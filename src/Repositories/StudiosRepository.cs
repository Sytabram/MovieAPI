using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public class StudiosRepository : IStudiosRepository
    {
        private readonly MovieAPIDataContext _context;
        public StudiosRepository(MovieAPIDataContext context)
        {
            _context = context;
        }

        public Task<StudioDetailViewModel> GetSingle(int id)
        {
            return _context.Studios.Include(t => t.Movies)
                                 .Select(t => new StudioDetailViewModel
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     Country = t.Country,
                                     Creation_date = t.Creation_date,
                                     Movies = t.Movies.Select(c => new MovieSummaryViewModel
                                     {
                                         Id = c.Id,
                                         Name = c.Name
                                     })
                                 }).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Studio> UpdateAsync(int id, UpdateStudioDto studioToUpdate)
        {
            var studio = await _context.Studios.FirstAsync(c => c.Id == id);

            studio.Name = studioToUpdate.Name;
            studio.Country = studioToUpdate.Country;
            studio.Creation_date = studioToUpdate.Creation_date;
            await _context.SaveChangesAsync();

            return studio;
        }

        public async Task<Studio> CreateAsync(CreateStudioDto studioToCreate)
        {
            var studioDb = new Studio();
            studioDb.Name = studioToCreate.Name;
            studioDb.Country = studioToCreate.Country;
            studioDb.Creation_date = studioToCreate.Creation_date;
            _context.Studios.Add(studioDb);
            await _context.SaveChangesAsync();

            return studioDb;
        }

        public Task<List<StudioSummaryViewModel>> Search(string name)
        {
            return _context.Studios.Where(c => string.IsNullOrWhiteSpace(name) || c.Name.Contains(name)).Select(t => 
            new StudioSummaryViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task<int> Delete(int id)
        {
            _context.Studios.Remove(await _context.Studios.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddMovieToStudio(int studioId, int movieId)
        {
            var studioDb = await _context.Studios.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == studioId);

            studioDb.Movies.Add(_context.Movies.Find(movieId));

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveMovieStudio(int studioId, int movieId)
        {
            var studioDb = await _context.Studios.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == studioId);

            studioDb.Movies.Remove(studioDb.Movies.First(c => c.Id == movieId));

            return await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsById(int id)
        {
            return _context.Studios.AnyAsync(c => c.Id == id);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _context.Studios.AnyAsync(c => c.Name == name);
        }
    }
}