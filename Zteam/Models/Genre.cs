using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; }
        public List<Game>? Games { get; set; }
    }
}
