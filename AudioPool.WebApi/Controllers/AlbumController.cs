using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.Dtos;
using AudioPool.Services.Interfaces;
using AudioPool.Models.InputModels;

//using AudioPool.Services.Interfaces;

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

        // http://localhost:5000/audiopool/albums/1
        [HttpGet("{id:int}", Name = "GetAlbumById")]
        public IActionResult GetAlbumById(int id)
        {
            return Ok(_albumService.GetAlbumById(id));
        }

        // http://localhost:5000/audiopool/albums/1/songs
        [HttpGet("{id:int}/songs", Name = "GetSongsByAlbumId")]
        public IActionResult GetSongsByAlbumId(int id)
        {
            return Ok(_albumService.GetSongsByAlbumId(id));
        }

        // -------------------------------------
        // --------- Authorized access ---------
        // -------------------------------------

        //Create Album
        [HttpPost("", Name = "CreateAlbum")]
        public IActionResult CreateAlbum([FromBody] AlbumInputModel album)
        {
            var newId = _albumService.CreateAlbum(album);
            return CreatedAtRoute("GetAlbumById", new { id = newId }, newId);
        }
        //Delete Album
        [HttpDelete("{id:int}", Name = "DeleteAlbum")]
        public IActionResult DeleteAlbum(int id)
        {
            _albumService.DeleteAlbum(id);
            return NoContent();
        }

    }
}