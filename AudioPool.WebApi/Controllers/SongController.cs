using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;
using AudioPool.WebApi.Attributes; // Add this using statement

namespace AudioPool.WebApi.Controllers
{
    [ApiController]
    [Route("songs")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        // -------------------------------------
        // -------- Unauthorized access --------
        // -------------------------------------

        [HttpGet("{id:int}", Name = "GetSongById")]
        public IActionResult GetSongById(int id)
        {
            try
            {
                return Ok(_songService.GetSongById(id));
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
        [HttpPost("", Name = "CreateSong")]
        public IActionResult CreateSong([FromBody] SongInputModel song)
        {
            try
            {
                var newId = _songService.CreateSong(song);
                return CreatedAtRoute("GetSongById", new { id = newId }, song);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiTokenAuthorization] // Add this attribute
        [HttpPut("{id:int}", Name = "UpdateSong")]
        public IActionResult UpdateSong([FromBody] SongInputModel song, int id)
        {
            try
            {
                _songService.UpdateSong(song, id);
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
        [HttpDelete("{id:int}", Name = "DeleteSong")]
        public IActionResult DeleteSong(int id)
        {
            try
            {
                _songService.DeleteSong(id);
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