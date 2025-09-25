
/*
○ Id (int)
○ Name (string)
○ Duration (timespan)
○ DateCreated (code-generated)
○ DateModified (code-generated) NULL
○ ModifiedBy (code-generated) NULL
○ Foreign keys
■ Album (one-to-many)
*/
namespace AudioPool.Repositories.Entities;
    /// <summary>
    /// Song entity representing a musical track.
    /// </summary>
public class Song
{
    /// <summary>
    /// Unique identifier for the song.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name of the song.
    /// </summary>
    public string Name { get; set; } = null!;
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
    public int AlbumId { get; set; }
}