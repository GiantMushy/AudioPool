using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.Dtos
{
    public class ArtistDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime DateOfStart { get; set; }
        public AlbumDto[] Albums { get; set; }
        public GenreDto[] Genres { get; set; }
    }
}