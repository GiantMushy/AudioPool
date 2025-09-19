using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class ArtistInputModel
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<GenreInputModel> Genres { get; set; }
        public ICollection<AlbumInputModel> Albums { get; set; }
    }
}