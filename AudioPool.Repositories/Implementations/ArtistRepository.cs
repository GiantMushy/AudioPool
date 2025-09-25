using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations
{
    public class ArtistRepository : IArtistRepository
    {
        public IEnumerable<ArtistDto> GetAllArtists()
        {
            throw new NotImplementedException();
        }

        public ArtistDetailsDto GetArtistById(int id)
        {
            throw new NotImplementedException();
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