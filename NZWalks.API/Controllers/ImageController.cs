using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        //POST: /api/Image/Upload
        [HttpPost]
        [Route("Upload")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Upload([FromForm] ImageDto imageDto)
        {
            ValidateFileUpload(imageDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _imageRepository.Upload(imageDto);

            return Ok();

        }



        //GET: /api/Image/GetAll
        [HttpGet]
        [Route("GetAllImages")]
        public async Task<IActionResult> GetAllImages()
        {

            var images = await _imageRepository.GetAllImages();
            return Ok(images);

        }




        private void ValidateFileUpload(ImageDto imageDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(imageDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (imageDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload smaller size file");
            }
        }


    }
}
