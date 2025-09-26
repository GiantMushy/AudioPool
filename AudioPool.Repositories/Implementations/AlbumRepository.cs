using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Entities;
using AudioPool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Implementations
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AudioDbContext _audioDbContext;
        public AlbumRepository(AudioDbContext audioDbContext)
        {
            _audioDbContext = audioDbContext;
        }
        public AlbumDetailsDto GetAlbumById(int id)
        {
            var album = _audioDbContext.Albums
                .Include(al => al.Artists)
                .Include(al => al.Songs)
                .Where(a => a.Id == id)
                .Select(a => new AlbumDetailsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ReleaseDate = a.ReleaseDate,
                    CoverImageUrl = a.CoverImageUrl ?? "",
                    Description = a.Description ?? "",
                    Artists = a.Artists.Select(ar => new ArtistDto
                    {
                        Id = ar.Id,
                        Name = ar.Name,
                        Bio = ar.Bio ?? "",
                        CoverImageUrl = ar.CoverImageUrl ?? "",
                        DateOfStart = ar.DateOfStart
                    }),
                    Songs = a.Songs.Select(s => new SongDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Duration = s.Duration
                    })
                }).FirstOrDefault();

            if (album == null)
            {
                return new AlbumDetailsDto();
            }
            return album;
        }

        public IEnumerable<SongDto> GetSongsByAlbumId(int albumId)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public int CreateAlbum(AlbumInputModel album)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public void DeleteAlbum(int id)
        {
            // Implementation here
            throw new NotImplementedException();
        }
    }
}