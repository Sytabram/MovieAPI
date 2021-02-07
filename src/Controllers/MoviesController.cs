using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Services;


namespace MovieAPI.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }
        private readonly IMoviesService _moviesService;

        [HttpGet("movies")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _moviesService.GetAll());
        }
        [HttpGet("movies/{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                return Ok(await _moviesService.GetSingle(id));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }

        [HttpPost("movies/{id}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] CUMovieDto modelDto)
        {
            if (id < 1) return BadRequest();
            if (!await _moviesService.ExistsById(id)) return NotFound();
            var movie = await _moviesService.Update(id, modelDto);
            return Ok(movie);
        }

        [HttpPost("movies")]
        public async Task<IActionResult> AddMovie(CUMovieDto newMovie)
        {
            try
            {
                var modelDb = await _moviesService.AddMovie(newMovie);
                return Created($"movie/{modelDb.Id}", modelDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("movies/search")]
        public async Task<IActionResult> Search(string name)
        {
            if (name == null) return Unauthorized();
            if (name.Length < 4) return Unauthorized();
            if (name == "teapot") return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status418ImATeapot);
            return Ok(await _moviesService.Search(name));
        }

        [HttpDelete("movies/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                await _moviesService.Delete(id);
                return Ok();
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("movies/{id}/poster")]
        public async Task<IActionResult> Images([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _moviesService.SetPoster(id, ms.ToArray());
            return Ok();
        }

        [HttpGet("movies/{id}/poster")]
        public async Task<IActionResult> Images([FromRoute] int id)
        {
            return File(await _moviesService.GetPoster(id), "image/jpeg");
        }
    }
}