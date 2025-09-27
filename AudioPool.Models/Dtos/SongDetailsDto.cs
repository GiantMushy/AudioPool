
namespace AudioPool.Models.Dtos
{
    public class SongDetailsDto : HyperMediaModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public AlbumDto Album { get; set; } = null!;
        public int TrackNumberOnAlbum { get; set; }
    }
}