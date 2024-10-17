namespace postit_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;

  public AlbumsController(AlbumsService albumsService)
  {
    _albumsService = albumsService;
  }
}
