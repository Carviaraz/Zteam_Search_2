using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        [Required(ErrorMessage = "จำเป็นต้องใส่ชื่อ ผู้พัฒนา")]
        [MaxLength(100, ErrorMessage = "ต้องมีขนาดไม่เกิน 100 ตัวอักษร")]
        public string DeveloperName { get; set; }

        public List<Game> GamesDeveloped { get; set; }
    }
}
