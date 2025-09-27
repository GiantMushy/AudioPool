using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using AudioPool.Models.Dtos;

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

        // http://localhost:5000/audiopool/artists?pageNumber=1&pageSize=25
        [HttpGet("", Name = "GetAllArtists")]
        public IActionResult GetAllArtists(
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1,
            [FromQuery][Range(1, 100)] int pageSize = 25
        )
        {
            return Ok(_artistService.GetAllArtists(pageNumber, pageSize));
        }

        // http://localhost:5000/audiopool/artists/1
        [HttpGet("{id:int}", Name = "GetArtistById")]
        public IActionResult GetArtistById(int id)
        {
            try
            {
                return Ok(_artistService.GetArtistById(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // http://localhost:5000/audiopool/artists/1/albums
        [HttpGet("{id:int}/albums", Name = "GetAlbumsByArtistId")]
        public IActionResult GetAlbumsByArtistId(int id)
        {
            try
            {
                return Ok(_artistService.GetAlbumsByArtistId(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // -------------------------------------
        // --------- Authorized access ---------
        // -------------------------------------

        //Create Artist
        [HttpPost("", Name = "CreateArtist")]
        public IActionResult CreateArtist([FromBody] ArtistInputModel input)
        {
            try
            {
                var newId = _artistService.CreateArtist(input);
                return CreatedAtRoute("GetArtistById", new { id = newId }, input);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update Artist
        [HttpPut("{id:int}", Name = "UpdateArtist")]
        public IActionResult UpdateArtist([FromBody] ArtistInputModel artist, int id)
        {
            try
            {
                _artistService.UpdateArtist(artist, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Link Artist to Genre
        [HttpPatch("{artistId:int}/genres/{genreId:int}", Name = "LinkArtistToGenre")]
        public IActionResult LinkArtistToGenre(int artistId, int genreId)
        {
            try
            {
                _artistService.LinkArtistToGenre(artistId, genreId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}