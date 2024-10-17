using System.ComponentModel.DataAnnotations;

namespace postit_dotnet.Models;

public class Picture
{
  public int Id { get; set; }

  [MaxLength(2000)]
  public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  public Profile Creator { get; set; }
}