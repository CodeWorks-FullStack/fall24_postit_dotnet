



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

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON pictures.creatorId = accounts.id
    WHERE pictures.albumId = @albumId;";

    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
    {
      picture.Creator = profile;
      return picture;
    }, new { albumId }).ToList();

    return pictures;
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = "SELECT * FROM pictures WHERE id = @pictureId;";

    Picture picture = _db.Query<Picture>(sql, new { pictureId }).FirstOrDefault();
    return picture;
  }

  internal void DeletePicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    if (rowsAffected == 0)
    {
      throw new Exception("No pictures deleted");
    }
    if (rowsAffected > 1)
    {
      throw new Exception($"{rowsAffected} pictures deleted and that ain't so good, partner");
    }
  }
}