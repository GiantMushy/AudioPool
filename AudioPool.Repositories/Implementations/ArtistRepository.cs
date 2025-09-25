using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
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
            throw new NotImplementedException();
        }

        public int CreateArtist(ArtistInputModel artist)
        {
            throw new NotImplementedException();
        }

        public void UpdateArtist(int id, ArtistInputModel artist)
        {
            throw new NotImplementedException();
        }

        public void LinkArtistToGenre(int artistId, int genreId)
        {
            throw new NotImplementedException();
        }

    }
}