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
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        private readonly IMovieService _movieService;

        [HttpGet("movies")]
        public IActionResult GetAll()
        {
            return Ok(_movieService.GetAll());
        }
        [HttpGet("movies/{id}")]
        public IActionResult GetSingle(int id)
        {
            if (id < 1) return BadRequest();
            var c = _movieService.GetSingle(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost("movies/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MoviePatchViewModel model)
        {
            if (id < 1) return BadRequest();
            if (model.Name.Length > 32) return BadRequest();
            if (_movieService.ExistsByName(model.Name)) return BadRequest();
            if (!_movieService.ExistsById(id)) return NotFound();

            return Ok(_movieService.Update(id, model));
        }

        [HttpPost("movies")]
        public IActionResult AddMovie(Movie newMovie)
        {
            if (newMovie.Id < 1) return BadRequest();
            if (newMovie.Name.Length > 32) return BadRequest();
            if (_movieService.ExistsByName(newMovie.Name)) return BadRequest();
            if (_movieService.ExistsById(newMovie.Id)) return BadRequest();

            _movieService.AddMovie(newMovie);

            return Created($"movies/{newMovie.Id}", newMovie);
        }

        [HttpGet("search/movies")]
        public IActionResult Search(string name)
        {
            if (name == null) return Unauthorized();
            if (name.Length < 4) return Unauthorized();
            if (name == "teapot") return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status418ImATeapot);
            return Ok(_movieService.Search(name));
        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {
            try {
                _movieService.Delete(id);
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
            _movieService.SetPoster(id, ms.ToArray());
            return Ok();
        }

        [HttpGet("movies/{id}/poster")]
        public IActionResult Images([FromRoute] int id)
        {
            return File(_movieService.GetPoster(id), "image/jpeg");
        }
    }
}