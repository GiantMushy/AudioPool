using System.ComponentModel.DataAnnotations;

namespace AudioPool.Models.InputModels
{
    public class SongInputModel
    {
        /// <summary>
        /// Name of the song.
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// Duration of the song.
        /// </summary>
        [Required]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The Id of the album to which the song belongs.
        /// </summary>
        [Required]
        public int AlbumId { get; set; }
    }
}