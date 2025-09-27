using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Entities;
using AudioPool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Implementations
{
    public class SongRepository : ISongRepository
    {
        private readonly AudioDbContext _audioDbContext;
        public SongRepository(AudioDbContext audioDbContext)
        {
            _audioDbContext = audioDbContext;
        }

        public SongDetailsDto GetSongById(int id)
        {
            // Get Id, Name, Dureation, album (as albumdto), and tracknumberonalbum generated
            var song = (from s in _audioDbContext.Songs
                        where s.Id == id
                        join al in _audioDbContext.Albums on s.AlbumId equals al.Id
                        select new SongDetailsDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Duration = s.Duration,
                            Album = new AlbumDto
                            {
                                Id = al.Id,
                                Name = al.Name,
                                ReleaseDate = al.ReleaseDate,
                                CoverImageUrl = al.CoverImageUrl,
                                Description = al.Description
                            },
                            // Track number = 1 + count of songs in same album with lower Id
                            TrackNumberOnAlbum = _audioDbContext.Songs
                                .Count(x => x.AlbumId == s.AlbumId && x.Id < s.Id) + 1
                        })
                        .FirstOrDefault();

            if (song == null)
            {
                throw new KeyNotFoundException($"Song not found (SongId: {id}).");
            }

            return song;
        }

        public int CreateSong(SongInputModel song)
        {
            var entity = new Song
            {
                Name = song.Name,
                Duration = song.Duration,
                DateCreated = DateTime.Now,
                AlbumId = song.AlbumId
            };

            _audioDbContext.Songs.Add(entity);
            _audioDbContext.SaveChanges();

            return entity.Id;
        }

        public void UpdateSong(int id, SongInputModel song)
        {
            var entity = _audioDbContext.Songs.FirstOrDefault(s => s.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Song not found (SongId: {id}).");
            }

            entity.Name = song.Name;
            entity.Duration = song.Duration;
            entity.DateModified = DateTime.Now;
            entity.ModifiedBy = "AudioPoolAdmin";
            entity.AlbumId = song.AlbumId;

            _audioDbContext.SaveChanges();
        }

        public void DeleteSong(int id)
        {
            var entity = _audioDbContext.Songs.FirstOrDefault(s => s.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Song not found (SongId: {id}).");
            }

            _audioDbContext.Songs.Remove(entity);
            _audioDbContext.SaveChanges();   
        }
    }
}