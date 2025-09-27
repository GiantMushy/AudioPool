using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces
{
    public interface IArtistService
    {
        PageDto<ArtistDto> GetAllArtists(int pageNumber, int pageSize);
        ArtistDetailsDto GetArtistById(int id);
        IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId);
        int CreateArtist(ArtistInputModel artist);
        void UpdateArtist(ArtistInputModel artist, int id);
        void LinkArtistToGenre(int artistId, int genreId);
    }
}