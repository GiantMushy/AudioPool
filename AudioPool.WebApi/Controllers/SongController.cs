using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;

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

        // http://localhost:5000/audiopool/songs/1
        [HttpGet("{id:int}", Name = "GetSongById")]
        public IActionResult GetSongById(int id)
        {
            return Ok(_songService.GetSongById(id));
        }

        // -------------------------------------
        // --------- Authorized access ---------
        // -------------------------------------

        [HttpPost("", Name = "CreateSong")]
        public IActionResult CreateSong([FromBody] SongInputModel song)
        {
            var newId = _songService.CreateSong(song);
            return CreatedAtRoute("GetSongById", new { id = newId }, song);
        }

        [HttpPut("{id:int}", Name = "UpdateSong")]
        public IActionResult UpdateSong([FromBody] SongInputModel song, int id)
        {
            _songService.UpdateSong(song, id);
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteSong")]
        public IActionResult DeleteSong(int id)
        {
            _songService.DeleteSong(id);
            return NoContent();
        }
    }
}