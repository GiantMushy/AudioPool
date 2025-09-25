using System.Collections.Generic;
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
            return _genreRepository.GetAllGenres();
        }

        public GenreDetailsDto GetGenreById(int id)
        {
            return _genreRepository.GetGenreById(id);
        }

        public int CreateGenre(GenreInputModel genre)
        {
            return _genreRepository.CreateGenre(genre);
        }
    }
}