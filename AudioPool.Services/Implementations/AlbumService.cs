using AudioPool.Models;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public AlbumDetailsDto GetAlbumById(int id)
        {
            var album = _albumRepository.GetAlbumById(id);

            album.Links.AddReference("self", $"api/albums/{id}");
            album.Links.AddReference("delete", $"api/albums/{id}");
            album.Links.AddReference("songs", $"api/albums/{id}/songs");
            album.Links.AddListReference("artists", album.Artists.Select(a => $"api/artists/{a.Id}"));

            return album;
        }

        public IEnumerable<SongDto> GetSongsByAlbumId(int albumId)
        {
            var songs = _albumRepository.GetSongsByAlbumId(albumId);

            foreach (var song in songs)
            {
                song.Links.AddReference("self", $"api/songs/{song.Id}");
                song.Links.AddReference("delete", $"api/songs/{song.Id}");
                song.Links.AddReference("edit", $"api/songs/{song.Id}");
                song.Links.AddReference("album", $"api/albums/{albumId}");
            }

            return songs;
        }

        public int CreateAlbum(AlbumInputModel album)
        {
            return _albumRepository.CreateAlbum(album);
        }

        public void DeleteAlbum(int id)
        {
            _albumRepository.DeleteAlbum(id);
        }
    }
}