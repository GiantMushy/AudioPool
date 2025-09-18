using Microsoft.AspNetCore.Mvc;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AudioPoolController : ControllerBase
{
    // --------- Unauthorized access ---------

    // http://localhost:5000/audiopool/genres
    [HttpGet("genres")]
    public ActionResult GetAllGenres()
    {
        return Ok("Hello from AudioPoolController");
    }

    // http://localhost:5000/audiopool/genres/1
    [HttpGet("genres/{id:int}")]
    public ActionResult GetGenreById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/artists
    [HttpGet("artists")]
    public ActionResult GetAllArtists()
    {
        return Ok();
    }

    // http://localhost:5000/audiopool/artists/1
    [HttpGet("artists/{id:int}")]
    public ActionResult GetArtistById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/artists/1/albums
    [HttpGet("artists/{id:int}/albums")]
    public ActionResult GetArtistAlbums(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/albums/1
    [HttpGet("albums/{id:int}")]
    public ActionResult GetAlbumById(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/albums/1/songs
    [HttpGet("albums/{id:int}/songs")]
    public ActionResult GetAlbumSongs(int id)
    {
        return Ok(id);
    }

    // http://localhost:5000/audiopool/songs/1
    [HttpGet("songs/{id:int}")]
    public ActionResult GetSongById(int id)
    {
        return Ok(id);
    }

    // -------------------------------------
    // --------- Authorized access ---------
    // -------------------------------------
    
    [HttpPost("genres")]
    public ActionResult CreateNewGenre()
    {
        return Created();
    }

    //Create Artist
    [HttpPost("artists")]
    public ActionResult CreateNewArtist()
    {
        return Created();
    }

    //Update Artist
    [HttpPut("artists/{id:int}")]
    public ActionResult UpdateArtist(int id)
    {
        return NoContent();
    }

    //Link Artist to Genre
    [HttpPatch("artists/{artistId:int}/genres/{genreId:int}")]
    public ActionResult LinkArtistToGenre(int artistId, int genreId)
    {
        return NoContent();
    }

    //Create Album
    [HttpPost("artists/{id:int}/albums")]
    public ActionResult CreateNewAlbum(int id)
    {
        return Created();
    }

    //Delete Album
    [HttpDelete("albums/{id:int}")]
    public ActionResult DeleteAlbum(int id)
    {
        return NoContent();
    }

    //Create Song
    [HttpPost("albums/{id:int}/songs")]
    public ActionResult CreateNewSong(int id)
    {
        return Created();
    }

    //Delete Song
    [HttpDelete("songs/{id:int}")]
    public ActionResult DeleteSong(int id)
    {
        return NoContent();
    }

    //Update Song
    [HttpPut("songs/{id:int}")]
    public ActionResult UpdateSong(int id)
    {
        return NoContent();
    }
}
