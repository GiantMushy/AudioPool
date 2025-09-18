using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.WebApi.Models.Dtos
{
    public class SongDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public AlbumDto Album { get; set; }
        public int TrackNumberOnAlbum { get; set; }
    }
}