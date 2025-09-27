using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Entities;
using AudioPool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AudioDbContext _audioDbContext;
        public GenreRepository(AudioDbContext audioDbContext)
        {
            _audioDbContext = audioDbContext;
        }
        public IEnumerable<GenreDto> GetAllGenres()
        {
            var genres = _audioDbContext.Genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return genres;
        }
        public List<int> GetArtistIdsByGenreId(int genreId)
        {
            return _audioDbContext.Genres
                .AsNoTracking()
                .Where(g => g.Id == genreId)
                .SelectMany(g => g.Artists.Select(a => a.Id))
                .ToList();
        }

        public GenreDetailsDto GetGenreById(int id)
        {
            var genre = _audioDbContext.Genres
                .Include(g => g.Artists)
                .Where(g => g.Id == id)
                .Select(g => new GenreDetailsDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    NumberOfArtists = g.Artists.Count()
                }).FirstOrDefault();

            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with id {id} not found.");
            }

            return genre;
        }

        public int CreateGenre(GenreInputModel genre)
        {
            var genreEntity = new Genre
            {
                Name = genre.Name,
                DateCreated = DateTime.Now,
                ModifiedBy = "AudioPoolAdmin"
            };

            _audioDbContext.Genres.Add(genreEntity);
            _audioDbContext.SaveChanges();

            return genreEntity.Id;
        }
    }
}