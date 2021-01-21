using Microsoft.AspNetCore.Mvc;
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
        
    }
}