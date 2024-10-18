namespace postit_dotnet.Controllers;

[Authorize] // all routes are authorized in this controller
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  public AccountController(AccountService accountService, Auth0Provider auth0Provider, WatchersService watchersService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _watchersService = watchersService;
  }
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly WatchersService _watchersService;


  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("watchers")] // account/watchers
  public async Task<ActionResult<List<WatcherAlbum>>> GetWatcherAlbumsByUserId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<WatcherAlbum> watcherAlbums = _watchersService.GetWatcherAlbumsByUserId(userInfo.Id);
      return Ok(watcherAlbums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
