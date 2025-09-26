using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Entities;
using AudioPool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Implementations
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AudioDbContext _audioDbContext;
        public ArtistRepository(AudioDbContext audioDbContext)
        {
            _audioDbContext = audioDbContext;
        }
        public IEnumerable<ArtistDto> GetAllArtists()
        {
            var artists = _audioDbContext.Artists.Select(a => new ArtistDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio ?? "",
                CoverImageUrl = a.CoverImageUrl ?? "",
                DateOfStart = a.DateOfStart
            }).ToList();

            return artists;
        }

        public ArtistDetailsDto GetArtistById(int id)
        {
            var artist = _audioDbContext.Artists
                .Include(a => a.Albums)
                .Include(a => a.Genres)
                .Where(a => a.Id == id)
                .Select(a => new ArtistDetailsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio ?? "",
                    CoverImageUrl = a.CoverImageUrl ?? "",
                    DateOfStart = a.DateOfStart,
                    Albums = a.Albums.Select(al => new AlbumDto
                    {
                        Id = al.Id,
                        Name = al.Name,
                        ReleaseDate = al.ReleaseDate,
                        CoverImageUrl = al.CoverImageUrl,
                        Description = al.Description
                    }),
                    Genres = a.Genres.Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                }).FirstOrDefault();

            if (artist == null)
            {
                return new ArtistDetailsDto();
            }
            return artist;
        }

        public IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId)
        {
            return _audioDbContext.Albums
                .Where(al => al.Artists.Any(a => a.Id == artistId))
                .Select(al => new AlbumDto
                {
                    Id = al.Id,
                    Name = al.Name,
                    ReleaseDate = al.ReleaseDate,
                    CoverImageUrl = al.CoverImageUrl,
                    Description = al.Description
                }).ToList();
        }

        public int CreateArtist(ArtistInputModel artist)
        {
            var entity = new Artist
            {
                Name = artist.Name,
                Bio = artist.Bio,
                CoverImageUrl = artist.CoverImageUrl,
                DateOfStart = artist.DateOfStart,
                DateCreated = DateTime.UtcNow
            };

            _audioDbContext.Artists.Add(entity);
            _audioDbContext.SaveChanges();

            return entity.Id;
        }

        public void UpdateArtist(int id, ArtistInputModel artist)
        {
            var entity = _audioDbContext.Artists.FirstOrDefault(a => a.Id == id);
            if (entity == null) { return; }

            entity.Name = artist.Name;
            entity.Bio = artist.Bio;
            entity.CoverImageUrl = artist.CoverImageUrl;
            entity.DateOfStart = artist.DateOfStart;
            entity.DateModified = DateTime.UtcNow;
            entity.ModifiedBy = "AudioPoolAdmin"; // Skv. verkefna skilgreiningu

            _audioDbContext.SaveChanges();
        }

        public void LinkArtistToGenre(int artistId, int genreId)
        {
            // Fetch the artist and genre entities, then add the genre to the artist's collection and add the artist to the genre's collection
            var artist = _audioDbContext.Artists
                .Include(a => a.Genres)
                .FirstOrDefault(a => a.Id == artistId);
            var genre = _audioDbContext.Genres
                .Include(g => g.Artists)
                .FirstOrDefault(g => g.Id == genreId);

            if (artist == null || genre == null) { return; }

            if (!artist.Genres.Contains(genre) && !genre.Artists.Contains(artist))
            {
                artist.Genres.Add(genre);
                genre.Artists.Add(artist);
            }

            _audioDbContext.SaveChanges();
        }

    }
}