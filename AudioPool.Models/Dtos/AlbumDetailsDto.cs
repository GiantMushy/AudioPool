using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.Dtos
{
    public class AlbumDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageUrl { get; set; } 
        public string? Description { get; set; }
        public IEnumerable<ArtistDto> Artists { get; set; } = [];
        public IEnumerable<SongDto> Songs { get; set; } = [];
        
    }
}