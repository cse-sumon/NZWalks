using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class RegionDto
    {

        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage ="Code has to be minimum of 3 characters")]
        [MaxLength(5, ErrorMessage = "Code has to be maximum of 5 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
