using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<GenreDto> GetAllGenres();
        GenreDetailsDto GetGenreById(int id);
        int CreateGenre(GenreInputModel genre);
    }
}