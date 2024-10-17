

using System.Net.NetworkInformation;

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

    // Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    // {
    //   album.Creator = profile;
    //   return album;
    // }, albumData).FirstOrDefault();
    Album album = _db.Query<Album, Profile, Album>(sql, JoinCreatorToAlbum, albumData).FirstOrDefault();
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creatorId = accounts.id;";
    List<Album> albums = _db.Query<Album, Profile, Album>(sql, JoinCreatorToAlbum).ToList();
    return albums;
  }

  private Album JoinCreatorToAlbum(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }
}

