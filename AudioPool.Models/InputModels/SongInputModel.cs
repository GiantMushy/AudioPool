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
        public string Name { get; set; }
        /// <summary>
        /// Duration of the song.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Date and time when the song was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Date and time when the song was last modified.
        /// </summary>
        public DateTime? DateModified { get; set; }
        /// <summary>
        /// Identifier of the user who last modified the song.
        /// </summary>
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// The album to which the song belongs.
        /// </summary>
        public AlbumInputModel Album { get; set; }
    }
}