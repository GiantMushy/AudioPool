using System.Collections.Generic;
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
            return _albumRepository.GetAlbumById(id);
        }

        public IEnumerable<SongDto> GetSongsByAlbumId(int albumId)
        {
            return _albumRepository.GetSongsByAlbumId(albumId);
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