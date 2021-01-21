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
}