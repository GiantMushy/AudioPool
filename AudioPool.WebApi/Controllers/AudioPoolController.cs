using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.Dtos;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AudioPoolController : ControllerBase
{
    // -------------------------------------
    // -------- Unauthorized access --------
    // -------------------------------------

    // http://localhost:5000/audiopool/genres
    [HttpGet("genres", Name = "GetAllGenres")]
    public ActionResult GetAllGenres()
    {
        return Ok("Hello from AudioPoolController");
    }

    // http://localhost:5000/audiopool/genres/1
    [HttpGet("genres/{id:int}", Name = "GetGenreById")]
    public ActionResult GetGenreById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/artists
    [HttpGet("artists", Name = "GetAllArtists")]
    public ActionResult GetAllArtists()
    {
        return Ok();
    }

    // http://localhost:5000/audiopool/artists/1
    [HttpGet("artists/{id:int}", Name = "GetArtistById")]
    public ActionResult GetArtistById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/artists/1/albums
    [HttpGet("artists/{id:int}/albums", Name = "GetAlbumsByArtistId")]
    public ActionResult GetAlbumsByArtistId(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/albums/1
    [HttpGet("albums/{id:int}", Name = "GetAlbumById")]
    public ActionResult GetAlbumById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/albums/1/songs
    [HttpGet("albums/{id:int}/songs", Name = "GetSongsByAlbumId")]
    public ActionResult GetSongsByAlbumId(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/songs/1
    [HttpGet("songs/{id:int}", Name = "GetSongById")]
    public ActionResult GetSongById(int id)
    {
        return Ok(id);
    }

    // -------------------------------------
    // --------- Authorized access ---------
    // -------------------------------------

    [HttpPost("genres", Name = "CreateGenre")]
    public ActionResult CreateGenre([FromBody] GenreDto input)
    {
        return CreatedAtAction("GetGenreById", new { id = 1 }, input);
    }

    //Create Artist
    [HttpPost("artists", Name = "CreateArtist")]
    public ActionResult CreateArtist([FromBody] ArtistDto input)
    {
        return Ok(input);
    }

    //Update Artist
    [HttpPut("artists/{id:int}", Name = "UpdateArtist")]
    public ActionResult UpdateArtist(int id)
    {
        return NoContent();
    }

    //Link Artist to Genre
    [HttpPatch("artists/{artistId:int}/genres/{genreId:int}", Name = "LinkArtistToGenre")]
    public ActionResult LinkArtistToGenre(int artistId, int genreId)
    {
        return NoContent();
    }

    //Create Album
    [HttpPost("artists/{id:int}/albums", Name = "CreateAlbum")]
    public ActionResult CreateAlbum(int id)
    {
        return Created();
    }

    //Delete Album
    [HttpDelete("albums/{id:int}", Name = "DeleteAlbum")]
    public ActionResult DeleteAlbum(int id)
    {
        return NoContent();
    }

    //Create Song
    [HttpPost("albums/{id:int}/songs", Name = "CreateSong")]
    public ActionResult CreateSong([FromBody] SongDto input)
    {
        return Ok(input);
    }

    //Delete Song
    [HttpDelete("songs/{id:int}", Name = "DeleteSong")]
    public ActionResult DeleteSong(int id)
    {
        return NoContent();
    }

    //Update Song
    [HttpPut("songs/{id:int}", Name = "UpdateSong")]
    public ActionResult UpdateSong(int id)
    {
        return NoContent();
    }
}
