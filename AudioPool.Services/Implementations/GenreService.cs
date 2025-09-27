using AudioPool.Models;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            var genre = _genreRepository.GetAllGenres();

            foreach (var g in genre)
            {
                g.Links.AddReference("self", $"api/genres/{g.Id}");
                g.Links.AddListReference("artists", _genreRepository.GetArtistIdsByGenreId(g.Id).Select(aid => $"api/artists/{aid}"));
            }

            return genre;
        }

        public GenreDetailsDto GetGenreById(int id)
        {
            var genre = _genreRepository.GetGenreById(id);

            genre.Links.AddReference("self", $"api/genres/{id}");
            genre.Links.AddListReference("artists", _genreRepository.GetArtistIdsByGenreId(id).Select(aid => $"api/artists/{aid}"));

            return genre;
        }

        public int CreateGenre(GenreInputModel genre)
        {
            return _genreRepository.CreateGenre(genre);
        }
    }
}