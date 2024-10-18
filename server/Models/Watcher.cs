namespace postit_dotnet.Models;

public class Watcher
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}

public class WatcherProfile : Profile
{
  // NOTE all members inherited from Profile class
  public int WatcherId { get; set; }
  public int AlbumId { get; set; }
}