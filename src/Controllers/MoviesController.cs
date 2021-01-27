using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            return Ok(_moviesService.GetAll());
        }
        [HttpGet("movies/{id}")]
        public IActionResult GetSingle(int id)
        {
            if (id < 1) return BadRequest();
            var c = _moviesService.GetSingle(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost("movies/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MoviePatchViewModel model)
        {
            if (id < 1) return BadRequest();
            if (model.Name.Length > 32) return BadRequest();
            if (_moviesService.ExistsByName(model.Name)) return BadRequest();
            if (!_moviesService.ExistsById(id)) return NotFound();

            return Ok(_moviesService.Update(id, model));
        }

        [HttpPost("movies")]
        public IActionResult AddMovie(Movie newMovie)
        {
            if (newMovie.Id < 1) return BadRequest();
            if (newMovie.Name.Length > 32) return BadRequest();
            if (_moviesService.ExistsByName(newMovie.Name)) return BadRequest();
            if (_moviesService.ExistsById(newMovie.Id)) return BadRequest();

            _moviesService.AddMovie(newMovie);

            return Created($"movies/{newMovie.Id}", newMovie);
        }

        [HttpGet("search/movies")]
        public IActionResult Search(string name)
        {
            if (name == null) return Unauthorized();
            if (name.Length < 4) return Unauthorized();
            if (name == "teapot") return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status418ImATeapot);
            return Ok(_moviesService.Search(name));
        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {
            try {
                _moviesService.Delete(id);
                return NoContent();
            } catch {
                return NoContent();
            }
        }

        [HttpPost("movies/{id}/poster")]
        public IActionResult Images([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            _moviesService.SetPoster(id, ms.ToArray());
            return Ok();
        }

        [HttpGet("movies/{id}/poster")]
        public IActionResult Images([FromRoute] int id)
        {
            return File(_moviesService.GetPoster(id), "image/jpeg");
        }
    }
}