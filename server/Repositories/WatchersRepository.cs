





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

    List<WatcherProfile> watchers = _db.Query<Watcher, WatcherProfile, WatcherProfile>(sql, (watcher, profile) =>
    {
      profile.AlbumId = watcher.AlbumId;
      profile.WatcherId = watcher.Id;
      return profile;
    },
     new { albumId }).ToList();
    return watchers;
  }
}
