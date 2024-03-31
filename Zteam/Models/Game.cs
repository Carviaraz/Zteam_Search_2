using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class Game
    {

        public int GameId { get; set; }
        [Required]
        [MaxLength(255)]
        public string GameName { get; set; }
        public string? GameInfo { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "ราคาต้องไม่น้อยกว่า 0")]
        public float Price { get; set; }
        public byte[]? GameImage { get; set; }
        public int? DeveloperId { get; set; }
        public String? ReleaseDate { get; set; }
        public string? Rating { get; set; }

        public List<Genre>? Genre { get; set; }
        public Developer? Developer { get; set; }

    }
}
