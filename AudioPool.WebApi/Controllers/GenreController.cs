using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Services.Interfaces;
using AudioPool.WebApi.Attributes; // Add this using statement

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

        [HttpGet("", Name = "GetAllGenres")]
        public ActionResult GetAllGenres()
        {
            return Ok(_genreService.GetAllGenres());
        }

        [HttpGet("{id:int}", Name = "GetGenreById")]
        public IActionResult GetGenreById(int id)
        {
            try
            {
                return Ok(_genreService.GetGenreById(id));
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
        [HttpPost("", Name = "CreateGenre")]
        public IActionResult CreateGenre([FromBody] GenreInputModel input)
        {
            try
            {
                var newId = _genreService.CreateGenre(input);
                return CreatedAtRoute("GetGenreById", new { id = newId }, input);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}