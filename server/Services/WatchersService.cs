




namespace postit_dotnet.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }
  private readonly WatchersRepository _repository;

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    Watcher watcher = _repository.CreateWatcher(watcherData);
    return watcher;
  }

  internal List<Profile> GetWatcherProfilesByAlbumId(int albumId)
  {
    List<Profile> watchers = _repository.GetWatcherProfilesByAlbumId(albumId);
    return watchers;
  }
}