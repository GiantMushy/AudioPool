using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations
{
    public class AlbumRepository : IAlbumRepository
    {
        public AlbumDetailsDto GetAlbumById(int id)
        {
            // Implementation here
            throw new NotImplementedException();
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