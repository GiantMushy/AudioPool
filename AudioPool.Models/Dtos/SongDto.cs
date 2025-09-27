
namespace AudioPool.Models.Dtos
{
    public class SongDto : HyperMediaModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan Duration { get; set; }
    }
}