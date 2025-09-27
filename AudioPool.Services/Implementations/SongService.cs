using AudioPool.Models;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public SongDetailsDto GetSongById(int id)
        {
            var song = _songRepository.GetSongById(id);

            song.Links.AddReference("self", $"api/songs/{id}");
            song.Links.AddReference("edit", $"api/songs/{id}");
            song.Links.AddReference("delete", $"api/songs/{id}");
            song.Links.AddReference("album", $"api/albums/{song.Album.Id}");

            return song;
        }

        public int CreateSong(SongInputModel song)
        {
            return _songRepository.CreateSong(song);
        }

        public void UpdateSong(SongInputModel song, int id)
        {
            _songRepository.UpdateSong(id, song);
        }

        public void DeleteSong(int id)
        {
            _songRepository.DeleteSong(id);
        }
    }
}