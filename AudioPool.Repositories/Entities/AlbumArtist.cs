using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Repositories.Entities
{
    public class AlbumArtist
    {
        public int AlbumsId { get; set; }
        public int ArtistsId { get; set; }

        // Navigation properties
        public Album? Album { get; set; }
        public Artist? Artist { get; set; }
    }
}