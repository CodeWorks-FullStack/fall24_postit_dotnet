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

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Watcher>> CreateWatcher([FromBody] Watcher watcherData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      watcherData.AccountId = userInfo.Id;
      Watcher watcher = _watchersService.CreateWatcher(watcherData);
      return Ok(watcher);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
