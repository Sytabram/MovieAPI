using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Category
    {
        public Category()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
        
    }
    public class CategorySummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieSummaryViewModel> Movies { get; set; }
    }

    public class UpdateCategoryDto 
    {
        public string Name { get; set; }
    }

    public class CreateCategoryDto 
    {
        public string Name { get; set; }
    }

    public class AddMovieToCategoryDto 
    {
        public int CategoryId { get; set; }
        public int MovieId { get; set; }
    }

    public class RemoveMovieFromCategoryDto 
    {
        public int CategoryId { get; set; }
        public int MovieId { get; set; }
    }
}