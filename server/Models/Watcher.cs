namespace postit_dotnet.Models;

// NOTE backing class for the many-to-many
public class Watcher
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}

// NOTE DTO for the profile view of the many-to-many
public class WatcherProfile : Profile
{
  // all members inherited from Profile class
  public int WatcherId { get; set; }
  public int AlbumId { get; set; }
}

// NOTE DTO for the album view of the many-to-many
public class WatcherAlbum : Album
{
  // all members inherited from Album class
  public int WatcherId { get; set; }
  public string AccountId { get; set; }
}