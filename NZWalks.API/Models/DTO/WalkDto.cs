using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "LengthInKm is Required")]
        [Range(0,50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required(ErrorMessage = "DifficultyId is Required")]
        public int DifficultyId { get; set; }

        [Required(ErrorMessage = "RegionId is Required")]
        public int RegionId { get; set; }

        public string? DifficultyName { get; set; }
        public string? RegionName { get; set; }
    }
}
