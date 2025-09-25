using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        public IEnumerable<GenreDto> GetAllGenres()
        {
            throw new System.NotImplementedException();
        }

        public GenreDetailsDto GetGenreById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int CreateGenre(GenreInputModel genre)
        {
            throw new System.NotImplementedException();
        }
    }
}