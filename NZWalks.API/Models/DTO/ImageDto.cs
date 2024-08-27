using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO
{
    public class ImageDto
    {
        public int Id { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string? FileExtension { get; set; }

        public long? FileSizeInBytes { get; set; }

        public string? FilePath { get; set; }
    }
}
