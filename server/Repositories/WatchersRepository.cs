








namespace postit_dotnet.Repositories;

public class WatchersRepository
{
  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO
    watchers(accountId, albumId)
    VALUES(@AccountId, @AlbumId);

    SELECT
    watchers.*,
    accounts.*
    FROM watchers
    JOIN accounts ON watchers.accountId = accounts.id
    WHERE watchers.id = LAST_INSERT_ID();";

    WatcherProfile watcherProfile = _db.Query<Watcher, WatcherProfile, WatcherProfile>(sql, (watcher, profile) =>
    {
      profile.WatcherId = watcher.Id;
      profile.AlbumId = watcher.AlbumId;
      return profile;
    },
    watcherData).FirstOrDefault();
    return watcherProfile;
  }

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    watchers.*,
    accounts.*
    FROM watchers
    JOIN accounts ON accounts.id = watchers.accountId
    WHERE watchers.albumId = @albumId;";

    // NOTE even though we are technically seeing account information for our second type, Dapper will fill in all possible properties on the DTO (data transfer object) for us with the data present on that section of the row
    // NOTE we take information from the many-to-many to fill in any additional properties on the DTO
    List<WatcherProfile> watcherProfiles = _db.Query<Watcher, WatcherProfile, WatcherProfile>(sql, (watcher, profile) =>
    {
      profile.AlbumId = watcher.AlbumId;
      profile.WatcherId = watcher.Id;
      return profile;
    },
     new { albumId }).ToList();
    return watcherProfiles;
  }

  internal List<WatcherAlbum> GetWatcherAlbumsByUserId(string userId)
  {
    string sql = @"
    SELECT
    watchers.*,
    albums.*,
    accounts.*
    FROM watchers
    JOIN albums ON albums.id = watchers.albumId
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE watchers.accountId = @userId;";

    List<WatcherAlbum> watcherAlbums = _db.Query<Watcher, WatcherAlbum, Profile, WatcherAlbum>(sql, (watcher, album, profile) =>
    {
      album.AccountId = watcher.AccountId;
      album.WatcherId = watcher.Id;
      album.Creator = profile;
      return album;
    }, new { userId }).ToList();

    return watcherAlbums;
  }

  internal Watcher GetWatcherById(int watcherId)
  {
    string sql = "SELECT * FROM watchers WHERE id = @watcherId;";

    Watcher watcher = _db.Query<Watcher>(sql, new { watcherId }).FirstOrDefault();
    return watcher;
  }

  internal void DeleteWatcher(int watcherId)
  {
    string sql = "DELETE FROM watchers WHERE id = @watcherId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { watcherId });

    switch (rowsAffected)
    {
      case 0:
        throw new Exception("Delete failed!");
      case 1:
        break;
      default:
        throw new Exception($"{rowsAffected} watchers were deleted and that ain't it big dawg");
    }
  }
}
