using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Repositories.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly NZWalksDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageRepository(NZWalksDbContext context, IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Upload(ImageDto imageDto)
        {
          
            var fileName = imageDto.FileName;
            var fileExtension = Path.GetExtension(imageDto.File.FileName);

            
            var imagesFolderPath = Path.Combine(_environment.ContentRootPath, "Images");

            // Check if the "Images" folder exists; if not, create it
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var localFilePath = Path.Combine(imagesFolderPath, $"{fileName}{fileExtension}");


            // upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await imageDto.File.CopyToAsync(stream);

           // var httpContext = _httpContextAccessor.HttpContext;
          //  var urlFilePath = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}/Images/{imageDto.FileName}{imageDto.FileExtension}";
           
            var urlFilePath = Path.Combine("Images", $"{fileName}{fileExtension}");

            var image = new Image
            {
                File = imageDto.File,
                FileExtension = fileExtension,
                FileSizeInBytes = imageDto.File.Length,
                FileName = fileName,
                FileDescription = imageDto.FileDescription,
                FilePath = urlFilePath
            };



            await _context.AddAsync(image);
            await _context.SaveChangesAsync();

        }



        public async Task<IEnumerable<ImageDto>> GetAllImages()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var domainPath = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";

            var images = await _context.Images
                        .Select(image => new ImageDto
                        {
                            FileName = image.FileName,
                            FileDescription = image.FileDescription,
                            FileSizeInBytes = image.FileSizeInBytes,
                            FileExtension = image.FileExtension,
                            FilePath = $"{domainPath}/{image.FilePath.Replace("\\", "/")}"
                        })              
                        .ToListAsync();


            return images;
        }
    }
}
