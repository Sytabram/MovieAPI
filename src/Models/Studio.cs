using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Studio
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(15)]
        public string Name { get; set; }
        public int contry { get; set; }
        public int creation_date { get; set; }
        public category Category { get; set; }
    }
}