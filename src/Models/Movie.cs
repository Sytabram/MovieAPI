using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name { get; set; }
        public byte[] Poster { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Time { get; set; }
        public Category Category { get; set; }
        public Studio Studio { get; set; }
    }
    
    
    public class MovieSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MoviePatchViewModel
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Studio Studio { get; set; }
    }

    public class CUMovieDTO
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