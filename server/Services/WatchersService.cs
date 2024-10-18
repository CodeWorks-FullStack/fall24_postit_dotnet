





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

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    List<WatcherProfile> watcherProfiles = _repository.GetWatcherProfilesByAlbumId(albumId);
    return watcherProfiles;
  }

  internal List<WatcherAlbum> GetWatcherAlbumsByUserId(string userId)
  {
    List<WatcherAlbum> watcherAlbums = _repository.GetWatcherAlbumsByUserId(userId);
    return watcherAlbums;
  }
}