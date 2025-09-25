using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        AlbumDetailsDto GetAlbumById(int id);
        IEnumerable<SongDto> GetSongsByAlbumId(int albumId);
        int CreateAlbum(AlbumInputModel album);
        void DeleteAlbum(int id);
    }
}