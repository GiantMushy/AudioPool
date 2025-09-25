using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        IEnumerable<ArtistDto> GetAllArtists();
        ArtistDetailsDto GetArtistById(int id);
        IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId);
        int CreateArtist(ArtistInputModel artist);
        void UpdateArtist(int id, ArtistInputModel artist);
        void LinkArtistToGenre(int artistId, int genreId);
    }
}