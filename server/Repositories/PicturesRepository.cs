namespace postit_dotnet.Repositories;

public class PicturesRepository
{
  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

}