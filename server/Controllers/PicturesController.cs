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
}