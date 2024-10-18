






namespace postit_dotnet.Repositories;

public class WatchersRepository
{
  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO
    watchers(accountId, albumId)
    VALUES(@AccountId, @AlbumId);

    SELECT * FROM watchers WHERE id = LAST_INSERT_ID();";

    Watcher watcher = _db.Query<Watcher>(sql, watcherData).FirstOrDefault();
    return watcher;
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
    albums.*
    FROM watchers
    JOIN albums ON albums.id = watchers.albumId
    WHERE watchers.accountId = @userId;";

    List<WatcherAlbum> watcherAlbums = _db.Query<Watcher, WatcherAlbum, WatcherAlbum>(sql, (watcher, album) =>
    {
      album.AccountId = watcher.AccountId;
      album.WatcherId = watcher.Id;
      return album;
    }, new { userId }).ToList();

    return watcherAlbums;
  }
}
