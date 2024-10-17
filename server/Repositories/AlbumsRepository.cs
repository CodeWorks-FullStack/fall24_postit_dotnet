
namespace postit_dotnet.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, coverImg, category, description, creatorId)
    VALUES(@Title, @CoverImg, @Category, @Description, @CreatorId);

    SELECT * FROM albums WHERE id = LAST_INSERT_ID();";

    Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();
    return album;
  }
}

