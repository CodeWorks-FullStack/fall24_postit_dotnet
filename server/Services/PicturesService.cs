


namespace postit_dotnet.Services;

public class PicturesService
{
  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }
  private readonly PicturesRepository _repository;
  // NOTE services can call to other services.
  // NEVER DIRECTLY ACCESS ANOTHER REPOSITORY FOR A DATA TYPE YOU DO NOT DIRECTLY DEAL WITH
  private readonly AlbumsService _albumsService;

  internal Picture CreatePicture(Picture pictureData)
  {
    Album album = _albumsService.GetAlbumById(pictureData.AlbumId);

    if (album.Archived) //album.Archived == true
    {
      throw new Exception($"{album.Title} has been archived and is no longer accepting pictures.");
    }

    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }

  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);

    if (picture == null)
    {
      throw new Exception($"Invalid picture id: {pictureId}");
    }

    return picture;
  }

  internal void DeletePicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);

    Album album = _albumsService.GetAlbumById(picture.AlbumId);

    if (album.Archived) //album.Archived == true
    {
      throw new Exception($"{album.Title} has been archived and that picture belongs to {album.Creator.Name} now, cowpoke.");
    }

    if (picture.CreatorId != userId)
    {
      throw new Exception("NOT YOUR PICTURE, BUCKAROO");
    }

    _repository.DeletePicture(pictureId);
  }
}
