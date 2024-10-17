
namespace postit_dotnet.Repositories;

public class PicturesRepository
{
  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(imgUrl, creatorId, albumId)
    VALUES(@ImgUrl, @CreatorId, @AlbumId);

    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON pictures.creatorId = accounts.id
    WHERE pictures.id = LAST_INSERT_ID();";

    Picture picture = _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
    {
      picture.Creator = profile;
      return picture;
    }, pictureData).FirstOrDefault();
    return picture;
  }
}