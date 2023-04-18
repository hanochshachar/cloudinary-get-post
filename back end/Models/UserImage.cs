using System.ComponentModel.DataAnnotations;

namespace cloudinaryImg.Models
{
    public class UserImage
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
