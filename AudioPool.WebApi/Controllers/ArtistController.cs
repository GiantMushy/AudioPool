using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;

namespace AudioPool.WebApi.Controllers
{
    [ApiController]
    [Route("artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService, IGenreService genreService)
        {
            _artistService = artistService;
        }

        // -------------------------------------
        // -------- Unauthorized access --------
        // -------------------------------------

        // http://localhost:5000/audiopool/artists
        [HttpGet("", Name = "GetAllArtists")]
        public IActionResult GetAllArtists()
        {
            return Ok(_artistService.GetAllArtists());
        }

        // http://localhost:5000/audiopool/artists/1
        [HttpGet("{id:int}", Name = "GetArtistById")]
        public IActionResult GetArtistById(int id)
        {
            return Ok(id);
        }

        // http://localhost:5000/audiopool/artists/1/albums
        [HttpGet("{id:int}/albums", Name = "GetAlbumsByArtistId")]
        public IActionResult GetAlbumsByArtistId(int id)
        {
            return Ok(_artistService.GetAlbumsByArtistId(id));
        }


        // -------------------------------------
        // --------- Authorized access ---------
        // -------------------------------------

        //Create Artist
        [HttpPost("", Name = "CreateArtist")]
        public IActionResult CreateArtist([FromBody] ArtistInputModel input)
        {
            var newId = _artistService.CreateArtist(input);
            return CreatedAtRoute("GetArtistById", new { id = newId }, input);
        }

        //Update Artist
        [HttpPut("{id:int}", Name = "UpdateArtist")]
        public IActionResult UpdateArtist([FromBody] ArtistInputModel artist, int id)
        {
            _artistService.UpdateArtist(artist, id);
            return NoContent();
        }

        //Link Artist to Genre
        [HttpPatch("{artistId:int}/genres/{genreId:int}", Name = "LinkArtistToGenre")]
        public IActionResult LinkArtistToGenre(int artistId, int genreId)
        {
            _artistService.LinkArtistToGenre(artistId, genreId);
            return NoContent();
        }
    }
}