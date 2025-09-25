
namespace AudioPool.Repositories.Entities;

public class ArtistGenre
{
    public int ArtistsId { get; set; }
    public int GenresId { get; set; }

    // Navigation properties
    public Artist? Artist { get; set; }
    public Genre? Genre { get; set; }
}