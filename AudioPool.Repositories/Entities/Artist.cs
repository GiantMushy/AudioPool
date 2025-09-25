using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioPool.Models.Entities;

/*
○ Id (int)
○ Name (string)
○ Bio (string) NULL
○ CoverImageUrl (string) NULL
○ DateStarted (string)
○ DateCreated (code-generated)
○ DateModified (code-generated) NULL
○ ModifiedBy (code-generated) NULL
○ Foreign keys
■ Genre (many-to-many)
■ Album (many-to-many)
*/
namespace AudioPool.Repositories.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<ArtistGenre> GenreLink { get; set; } = [];
        public ICollection<AlbumArtist> AlbumLink { get; set; } = [];
    }
}