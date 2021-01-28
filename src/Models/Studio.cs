using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Studio
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name { get; set; }
        public int Country { get; set; }
        public int Creation_date { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
    public class StudioDto
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name{get; set;}
        public int Country { get; set; }
        public int Creation_date { get; set; }
    }
}