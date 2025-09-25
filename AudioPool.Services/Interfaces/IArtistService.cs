using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<ArtistDto> GetAllArtists();
        ArtistDetailsDto GetArtistById(int id);
        IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId);
        int CreateArtist(ArtistInputModel artist);
        void UpdateArtist(ArtistInputModel artist, int id);
        void LinkArtistToGenre(int artistId, int genreId);
    }
}