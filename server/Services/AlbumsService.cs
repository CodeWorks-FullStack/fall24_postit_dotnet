

using Microsoft.AspNetCore.Http.HttpResults;

namespace postit_dotnet.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> GetAllAlbums()

  {
    List<Album> albums = _repository.GetAllAlbums();
    return albums;
  }
}
