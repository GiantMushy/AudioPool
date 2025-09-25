using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;

namespace AudioPool.WebApi.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        // http://localhost:5000/audiopool/genres
        [HttpGet("", Name = "GetAllGenres")]
        public ActionResult GetAllGenres()
        {
            return Ok(_genreService.GetAllGenres());
        }

        // http://localhost:5000/audiopool/genres/1
        [HttpGet("{id:int}", Name = "GetGenreById")]
        public IActionResult GetGenreById(int id)
        {
            return Ok(_genreService.GetGenreById(id));
        }

        // -------------------------------------
        // --------- Authorized access ---------
        // -------------------------------------

        [HttpPost("", Name = "CreateGenre")]
        public IActionResult CreateGenre([FromBody] GenreInputModel input)
        {
            var newId = _genreService.CreateGenre(input);
            return CreatedAtRoute("GetGenreById", new { id = newId }, input);
        }
    }
}