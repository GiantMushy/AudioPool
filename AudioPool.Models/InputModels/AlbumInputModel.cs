using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class AlbumInputModel
    {
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public ICollection<int> ArtistIds { get; set; } = [];
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
    }
}