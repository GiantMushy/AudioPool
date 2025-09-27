using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<GenreDto> GetAllGenres();
        List<int> GetArtistIdsByGenreId(int genreId);
        GenreDetailsDto GetGenreById(int id);
        int CreateGenre(GenreInputModel genre);
    }
}