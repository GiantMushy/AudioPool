using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class SongInputModel
    {
        /// <summary>
        /// Name of the song.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Duration of the song.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// The Id of the album to which the song belongs.
        /// </summary>
        public int AlbumId { get; set; }
    }
}