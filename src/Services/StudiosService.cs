using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Services
{
    public class StudiosService : IStudiosService
    {
        private readonly IStudiosRepository _studiosRepository;
        public StudiosService(IStudiosRepository studiosRepository)
        {
            _studiosRepository = studiosRepository;
        }

        public async Task<StudioDetailViewModel> GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var studioDb = await _studiosRepository.GetSingle(id);

            return studioDb;
        }

        public async Task<Studio> UpdateAsync(int id, UpdateStudioDto studioToUpdate)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (studioToUpdate == null)
                throw new ArgumentNullException(nameof(studioToUpdate));

            if (studioToUpdate.Name.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(studioToUpdate.Name), studioToUpdate.Name, "Studio name length cannot be greater than 35.");

            if (!await _studiosRepository.ExistsById(id))
                throw new DataNotFoundException($"Studio Id:{id} doesn't exists.");

            if (await _studiosRepository.ExistsByName(studioToUpdate.Name))
                throw new ArgumentException(nameof(studioToUpdate.Name), $"Studio {studioToUpdate.Name} already exists.");

            return await _studiosRepository.UpdateAsync(id, studioToUpdate);
        }

        public async Task<Studio> CreateAsync(CreateStudioDto studioToCreate)
        {
            if (studioToCreate == null)
                throw new ArgumentNullException(nameof(studioToCreate));

            if (studioToCreate.Name.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(studioToCreate.Name), studioToCreate.Name, "Studio name length cannot be greater than 35.");

            if (await _studiosRepository.ExistsByName(studioToCreate.Name))
                throw new ArgumentException(nameof(studioToCreate.Name), $"Studio {studioToCreate.Name} already exists.");

            var modelDb = await _studiosRepository.CreateAsync(studioToCreate);

            return modelDb;
        }

        public Task<List<StudioSummaryViewModel>> GetAll(string filterByName)
        {
            if (filterByName?.Length < 4)
                throw new ArgumentOutOfRangeException("Studio name length must be greater than 3.");

            return _studiosRepository.Search(filterByName);
        }

        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var result = await _studiosRepository.Delete(id);

            //If result == 1, one entity has been deleted from the database
            if (result == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> AddMovieToStudio(AddMovieToStudioDto addMovieToStudio)
        {
            if (addMovieToStudio == null)
                throw new ArgumentNullException(nameof(addMovieToStudio));

            if (addMovieToStudio.StudioId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMovieToStudio.StudioId), addMovieToStudio.StudioId, "Studio Id cannot be lower than 1.");

            if (addMovieToStudio.MovieId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMovieToStudio.MovieId), addMovieToStudio.MovieId, "Movie Id cannot be lower than 1.");

            if (!await _studiosRepository.ExistsById(addMovieToStudio.StudioId))
                throw new DataNotFoundException($"Studio Id:{addMovieToStudio.StudioId} doesn't exists.");

            // Ici, si on avait un CharacterRepository on devrait checker si le CharacterId existe dans la db
            // if (!_characterRepository.ExistsById(addCharacterToTeam.CharacterId))
            //     throw new DataNotFoundException($"CharacterId:{addCharacterToTeam.CharacterId} doesn't exists.");

            var result = await _studiosRepository.AddMovieToStudio(addMovieToStudio.StudioId, addMovieToStudio.MovieId);

            if (result == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> RemoveMovieFromStudio(RemoveMovieFromStudioDto removeMovieFromStudio)
        {
            if (removeMovieFromStudio == null)
                throw new ArgumentNullException(nameof(removeMovieFromStudio));

            if (removeMovieFromStudio.StudioId < 1)
                throw new ArgumentOutOfRangeException(nameof(removeMovieFromStudio.StudioId), removeMovieFromStudio.StudioId, "Category Id cannot be lower than 1.");

            if (removeMovieFromStudio.MovieId < 1)
                throw new ArgumentOutOfRangeException(nameof(removeMovieFromStudio.MovieId), removeMovieFromStudio.MovieId, "Movie Id cannot be lower than 1.");

            if (!await _studiosRepository.ExistsById(removeMovieFromStudio.StudioId))
                throw new DataNotFoundException($"Studio Id:{removeMovieFromStudio.StudioId} doesn't exists.");

            // Ici, si on avait un CharacterRepository on devrait checker si le CharacterId existe dans la db
            // if (!_characterRepository.ExistsById(addCharacterToTeam.CharacterId))
            //     throw new DataNotFoundException($"CharacterId:{addCharacterToTeam.CharacterId} doesn't exists.");

            var result = await _studiosRepository.RemoveMovieStudio(removeMovieFromStudio.StudioId, removeMovieFromStudio.MovieId);

            if (result == 1)
                return true;
            else
                return false;
        }
    }
}