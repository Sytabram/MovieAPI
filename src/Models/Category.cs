using System.Collections.Generic;

namespace MovieAPI.Models
{
    public class category
    {
        public category()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
        
    }
}