using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IImageRepository
    {
         Task Upload(ImageDto imageDto);

        Task<IEnumerable<ImageDto>> GetAllImages();
    }
}
