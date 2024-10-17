
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

    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creatorId = accounts.id
    WHERE albums.id = LAST_INSERT_ID();";

    Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).FirstOrDefault();
    return album;
  }
}

