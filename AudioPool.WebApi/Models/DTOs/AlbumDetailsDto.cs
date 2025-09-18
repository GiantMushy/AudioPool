using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.WebApi.Models.Dtos
{
    public class AlbumDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public ArtistDto[] Artists { get; set; }
        public SongDto[] Songs { get; set; }
        
    }
}