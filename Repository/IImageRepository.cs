namespace TripGuide.Repository
{
    public interface IImageRepository
    {
        string Upload(IFormFile file);
    }
}