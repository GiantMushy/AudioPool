using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using AudioPool.Models.Dtos;
using AudioPool.WebApi.Attributes; // Add this using statement

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

        [HttpGet("", Name = "GetAllArtists")]
        public IActionResult GetAllArtists(
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1,
            [FromQuery][Range(1, 100)] int pageSize = 25
        )
        {
            return Ok(_artistService.GetAllArtists(pageNumber, pageSize));
        }

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

        [ApiTokenAuthorization] // Add this attribute
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

        [ApiTokenAuthorization] // Add this attribute
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

        [ApiTokenAuthorization] // Add this attribute
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