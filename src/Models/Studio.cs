using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Studio
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(35)]
        public string Name { get; set; }
        public string Country { get; set; }
        public int Creation_date { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
    public class StudioSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudioDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Creation_date { get; set; }
        public IEnumerable<MovieSummaryViewModel> Movies { get; set; }
    }

    public class UpdateStudioDto 
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Creation_date { get; set; }
    }

    public class CreateStudioDto 
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Creation_date { get; set; }
    }

    public class AddMovieToStudioDto 
    {
        public int StudioId { get; set; }
        public int MovieId { get; set; }
    }

    public class RemoveMovieFromStudioDto 
    {
        public int StudioId { get; set; }
        public int MovieId { get; set; }
    }
    public class StudioDto
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(35)]
        public string Name{get; set;}
        public string Country { get; set; }
        public int Creation_date { get; set; }
    }
}