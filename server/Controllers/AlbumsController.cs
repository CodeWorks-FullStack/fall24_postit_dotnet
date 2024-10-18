namespace postit_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider, PicturesService picturesService, WatchersService watchersService)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
    _watchersService = watchersService;
  }
  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth0Provider;


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAllAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAllAlbums();
      return Ok(albums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(albumId, userInfo.Id);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/pictures")] // api/albums/1/pictures
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/watchers")]
  public ActionResult<List<WatcherProfile>> GetWatcherProfilesByAlbumId(int albumId)
  {
    try
    {
      List<WatcherProfile> watcherProfiles = _watchersService.GetWatcherProfilesByAlbumId(albumId);
      return Ok(watcherProfiles);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}
