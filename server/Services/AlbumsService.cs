

using Microsoft.AspNetCore.Http.HttpResults;

namespace postit_dotnet.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album albumToArchive = GetAlbumById(albumId);

    if (albumToArchive.CreatorId != userId)
    {
      throw new Exception("NOT YOUR ALBUM, FRIENDO");
    }

    // albumToArchive.Archived = true;
    albumToArchive.Archived = !albumToArchive.Archived;

    _repository.ArchiveAlbum(albumToArchive);

    return albumToArchive;
  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);
    if (album == null)
    {
      throw new Exception($"Invalid album id: {albumId}");
    }
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    List<Album> albums = _repository.GetAllAlbums();
    return albums;
  }
}
