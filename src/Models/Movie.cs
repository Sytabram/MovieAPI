using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Time { get; set; }
        public int Genres { get; set; }
        public category Category { get; set; }
    }


}