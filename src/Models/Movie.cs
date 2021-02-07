using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(35)]
        public string Name { get; set; }
        public byte[] Poster { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Time { get; set; }
        public  ICollection<Category> Category {get; set;}
        public  ICollection<Studio> Studio {get; set;}
    }
    
    
    public class MovieSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MoviePatchViewModel
    {
        public string Name { get; set; }
        public  ICollection<Category> Category {get; set;}
        public  ICollection<Studio> Studio {get; set;}
    }
    public class MovieDto
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(35)]
        public string Name { get; set; }
        public byte[] Poster { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Time { get; set; }
        public virtual ICollection<CategoryDto> Category {get; set;}
        public virtual ICollection<StudioDto> Studio {get; set;}
    }
    public class CUMovieDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Time { get; set; }
        public List<int> CategoryID{get; set; }
        public List<int> StoudioID{get; set; }
    }
}