
namespace postit_dotnet.Services;

public class PicturesService
{
  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }
  private readonly PicturesRepository _repository;

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }
}