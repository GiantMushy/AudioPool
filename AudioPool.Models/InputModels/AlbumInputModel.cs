using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class AlbumInputModel
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<ArtistInputModel> Artists { get; set; }
        public ICollection<SongInputModel> Songs { get; set; }
    }
}