

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

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creatorId = accounts.id
    WHERE albums.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, JoinCreatorToAlbum, new { albumId }).FirstOrDefault();
    return album;
  }

  private Album JoinCreatorToAlbum(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }

  internal void ArchiveAlbum(Album albumToArchive)
  {
    string sql = @"
    UPDATE
    albums
    SET archived = @Archived
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, albumToArchive);

    if (rowsAffected == 0)
    {
      throw new Exception("No albums were updated");
    }
    if (rowsAffected > 1)
    {
      throw new Exception($"{rowsAffected} albums were updated, and that is bad! Only 1 album should have been updated! Check your sql syntax for errors, or ask Jake Overall for help!");
    }
  }
}

