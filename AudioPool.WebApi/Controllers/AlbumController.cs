using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.Dtos;
using AudioPool.Services.Interfaces;
using AudioPool.Models.InputModels;
using AudioPool.WebApi.Attributes; 

namespace AudioPool.WebApi.Controllers
{
    [ApiController]
    [Route("albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;

        public AlbumController(IAlbumService albumService, ISongService songService)
        {
            _albumService = albumService;
            _songService = songService;
        }

        // -------------------------------------
        // -------- Unauthorized access --------
        // -------------------------------------

        [HttpGet("{id:int}", Name = "GetAlbumById")]
        public IActionResult GetAlbumById(int id)
        {
            try
            {
                return Ok(_albumService.GetAlbumById(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}/songs", Name = "GetSongsByAlbumId")]
        public IActionResult GetSongsByAlbumId(int id)
        {
            try
            {
                return Ok(_albumService.GetSongsByAlbumId(id));
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
        [HttpPost("", Name = "CreateAlbum")]
        public IActionResult CreateAlbum([FromBody] AlbumInputModel album)
        {
            try
            {
                var newId = _albumService.CreateAlbum(album);
                return CreatedAtRoute("GetAlbumById", new { id = newId }, newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiTokenAuthorization] // Add this attribute
        [HttpDelete("{id:int}", Name = "DeleteAlbum")]
        public IActionResult DeleteAlbum(int id)
        {
            try
            {
                _albumService.DeleteAlbum(id);
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