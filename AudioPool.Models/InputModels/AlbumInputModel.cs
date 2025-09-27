using System.ComponentModel.DataAnnotations;

namespace AudioPool.Models.InputModels
{
    public class AlbumInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public ICollection<int> ArtistIds { get; set; } = [];

        [Url]
        public string? CoverImageUrl { get; set; }

        [MinLength(10)]
        public string? Description { get; set; }
    }
}