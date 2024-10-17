namespace postit_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
  public PicturesController(PicturesService picturesService, Auth0Provider auth0Provider)
  {
    _picturesService = picturesService;
    _auth0Provider = auth0Provider;
  }
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth0Provider;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{pictureId}")]
  public async Task<ActionResult<string>> DeletePicture(int pictureId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _picturesService.DeletePicture(pictureId, userInfo.Id);
      return Ok("Picture was deleted!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}