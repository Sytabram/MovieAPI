using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    public class StudiosController : ControllerBase
    {
        private readonly IStudiosService _studiosService;
        private readonly IMoviesService _moviesService;

        public StudiosController(IStudiosService studiosService, IMoviesService moviesService)
        {
            _studiosService = studiosService;
            _moviesService = moviesService;
        }
        
        [HttpGet("studios")]
        public async Task<IActionResult> SearchAsync([FromQuery]string name)
        {
            try
            {
                return Ok(await _studiosService.GetAll(name));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("studios/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                var result = await _studiosService.GetSingle(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("studios/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateStudioDto model)
        {
            try
            {
                return Ok(await _studiosService.UpdateAsync(id, model));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("studios")]
        public async Task<IActionResult> CreateAsync(CreateStudioDto model)
        {
            try
            {
                var modelDb = await _studiosService.CreateAsync(model);

                return Created($"studios/{modelDb.Id}", modelDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("studios/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studiosService.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("studios/movies")]
        public async Task<IActionResult> RemoveMovieFromStudioAsync(RemoveMovieFromStudioDto removeMovieFromStudio)
        {
            try
            {
                await _studiosService.RemoveMovieFromStudio(removeMovieFromStudio);
                return Ok();
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("studios/movies")]
        public async Task<IActionResult> AddMovieToStudioAsync(AddMovieToStudioDto addMovieToStudio)
        {
            try
            {
                await _studiosService.AddMovieToStudio(addMovieToStudio);
                return Ok();
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}