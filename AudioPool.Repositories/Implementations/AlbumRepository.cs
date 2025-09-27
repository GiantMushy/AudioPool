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
                throw new KeyNotFoundException($"Album with id {id} not found.");
            }
            return album;
        }

        public IEnumerable<SongDto> GetSongsByAlbumId(int albumId)
        {
            var songs = _audioDbContext.Songs
                .Where(s => s.AlbumId == albumId)
                .Select(s => new SongDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Duration = s.Duration
                }).ToList();

            if (!songs.Any())
            {
                throw new KeyNotFoundException($"No songs found for album with id {albumId}.");
            }

            return songs;
        }

        public int CreateAlbum(AlbumInputModel album)
        {
            var entity = new Album
            {
                Name = album.Name,
                ReleaseDate = album.ReleaseDate,
                CoverImageUrl = album.CoverImageUrl,
                Description = album.Description,
                DateCreated = DateTime.UtcNow
            };

            _audioDbContext.Albums.Add(entity);
            _audioDbContext.SaveChanges();

            return entity.Id;
        }

        public void DeleteAlbum(int id)
        { // This must also delete all songs associated with the album due to foreign key constraints
            var entity = _audioDbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Album not found (AlbumId: {id}).");
            }

            var songs = _audioDbContext.Songs.Where(s => s.AlbumId == id).ToList();

            _audioDbContext.Songs.RemoveRange(songs);
            _audioDbContext.Albums.Remove(entity);
            _audioDbContext.SaveChanges();
        }
    }
}