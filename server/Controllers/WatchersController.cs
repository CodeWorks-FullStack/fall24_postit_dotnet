namespace postit_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchersController : ControllerBase
{
  public WatchersController(WatchersService watchersService, Auth0Provider auth0Provider)
  {
    _watchersService = watchersService;
    _auth0Provider = auth0Provider;
  }
  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth0Provider;
}
