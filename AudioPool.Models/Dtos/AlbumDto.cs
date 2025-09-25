using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.Dtos
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
    }
}