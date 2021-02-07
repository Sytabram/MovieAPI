using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMoviesService _moviesService;

        public CategoriesController(ICategoriesService categoriesService, IMoviesService moviesService)
        {
            _categoriesService = categoriesService;
            _moviesService = moviesService;
        }
        
        [HttpGet("categories")]
        public async Task<IActionResult> SearchAsync([FromQuery]string name)
        {
            try
            {
                return Ok(await _categoriesService.GetAll(name));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                var result = await _categoriesService.GetSingle(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("categories/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCategoryDto model)
        {
            try
            {
                return Ok(await _categoriesService.UpdateAsync(id, model));
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

        [HttpPost("categories")]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto model)
        {
            try
            {
                var modelDb = await _categoriesService.CreateAsync(model);

                return Created($"categories/{modelDb.Id}", modelDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoriesService.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("categories/movies")]
        public async Task<IActionResult> RemoveMovieFromCategoryAsync(RemoveMovieFromCategoryDto removeMovieFromCategory)
        {
            try
            {
                await _categoriesService.RemoveMovieFromCategory(removeMovieFromCategory);
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

        [HttpPost("categories/movies")]
        public async Task<IActionResult> AddMovieToCategoryAsync(AddMovieToCategoryDto addMovieToCategory)
        {
            try
            {
                await _categoriesService.AddMovieToCategory(addMovieToCategory);
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