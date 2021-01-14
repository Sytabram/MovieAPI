using System.Collections.Generic;

namespace MovieAPI.Models
{
    public class category
    {
        public category()
        {
            Movies = new List<Movie>();
        }
        public List<Movie> Movies { get; set; }
    }
}