namespace postit_dotnet.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }
  private readonly WatchersRepository _repository;

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    WatcherProfile watcherProfile = _repository.CreateWatcher(watcherData);
    return watcherProfile;
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

  private Watcher GetWatcherById(int watcherId)
  {
    Watcher watcher = _repository.GetWatcherById(watcherId);
    if (watcher == null)
    {
      throw new Exception($"Invalid watcher id: {watcherId}");
    }
    return watcher;
  }

  internal void DeleteWatcher(int watcherId, string userId)
  {
    Watcher watcher = GetWatcherById(watcherId);

    if (watcher.AccountId != userId)
    {
      throw new Exception("NOT YOUR WATCHER TO DELETE, LIL DOGGY");
    }

    _repository.DeleteWatcher(watcherId);
  }
}