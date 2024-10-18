





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

  internal List<Watcher> GetWatcherProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    watchers.*
    FROM watchers
    WHERE watchers.albumId = @albumId;";

    List<Watcher> watchers = _db.Query<Watcher>(sql, new { albumId }).ToList();
    return watchers;
  }
}
