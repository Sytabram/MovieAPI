using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<List<MovieDto>> GetAll()
        {
            var movieDb = await _moviesRepository.GetAllMovie();
            return movieDb;
        }
        public async Task<MovieDto> GetSingle(int id)
        {
            var movieDb = await _moviesRepository.GetSingleMovie(id);
            return movieDb;
        }

        public async Task<MovieDto> Update(int id, CUMovieDto model)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if(model.Name.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(model.Name), model.Name, "Name length cannot be bigger than 35.");
            if(model.Year.ToString().Length != 4)
                throw new ArgumentOutOfRangeException(nameof(model.Year), model.Year, "Year length must be 4.");
            if(model.Rating < 1 && model.Rating > 5)
                throw new ArgumentOutOfRangeException(nameof(model.Rating), model.Rating, "Rating must be between 1 and 5");
        
            return await _moviesRepository.UpdateMovie(id, model);
        }

        public async Task<MovieDto> AddMovie(CUMovieDto newMovie)
        {
            if(newMovie == null)
                throw new ArgumentNullException(nameof(newMovie));
            if(newMovie.Name.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(newMovie.Name), newMovie.Name, "Name length cannot be bigger than 35.");
            if(newMovie.Year.ToString().Length != 4)
                throw new ArgumentOutOfRangeException(nameof(newMovie.Year), newMovie.Year, "Year length must be 4.");
            if(newMovie.Rating < 1 && newMovie.Rating > 5)
                throw new ArgumentOutOfRangeException(nameof(newMovie.Rating), newMovie.Rating, "Rating must be between 1 and 5");
        
            var modelDb = await _moviesRepository.AddMovie(newMovie);
            return modelDb;
        }

        public async Task<List<Movie>> Search(string name)
        {
            return await _moviesRepository.Search(name);
        }

        public async Task<bool> Delete(int id)
        {
            if(id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var result = await _moviesRepository.DeleteMovie(id);

            //if result == 1 entity has been deleted from the db
            if (result == 1)
                return true;
            else
                return false;
        }


        public async Task<bool> SetPoster(int id, byte[] image)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            if(image == null)
                throw new ArgumentNullException(nameof(image));

            return await _moviesRepository.SetPoster(id, image);
        }

        public async Task<byte[]> GetPoster(int id)
        {
            var ImageDb = await _moviesRepository.GetPoster(id);
            return ImageDb;
        }

        public async Task<bool> ExistsById(int id)
        {
            var existDb = await _moviesRepository.ExistsById(id);
            return existDb;
        }

        public async Task<bool> ExistsByName(string name)
        {
            var existsByName = await _moviesRepository.ExistsByName(name);
            return existsByName;
        }
    }
}